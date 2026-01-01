#!/bin/bash

# Define paths
PROJECT_ROOT=$(git rev-parse --show-toplevel)
API_PROJECT_PATH="$PROJECT_ROOT/Kr.__PROJECT_NAME__.Api/Kr.__PROJECT_NAME__.Api.csproj"
OUTPUT_DIR="$PROJECT_ROOT/.spec"
OUTPUT_FILE="$OUTPUT_DIR/openapi.yml"

# Define Tool Manifest Paths
# We want the manifest in .scripts/.config/dotnet-tools.json
# So the "root" for the manifest is .scripts
MANIFEST_ROOT="$PROJECT_ROOT/.scripts"
MANIFEST_FILE="$MANIFEST_ROOT/.config/dotnet-tools.json"

# Ensure output directory exists
mkdir -p "$OUTPUT_DIR"
mkdir -p "$MANIFEST_ROOT"

# Initialize local dotnet tool manifest inside .scripts if it doesn't exist
if [ ! -f "$MANIFEST_FILE" ]; then
    echo "Initializing dotnet tool manifest in .scripts..."
    old_pwd=$(pwd)
    cd "$MANIFEST_ROOT" || exit
    # This creates .config/dotnet-tools.json inside MANIFEST_ROOT
    dotnet new tool-manifest --force
    cd "$old_pwd" || exit
fi

# Install or restore Swashbuckle CLI tool using the specific manifest
echo "Restoring Swashbuckle CLI..."
# Check if tool is installed (grep logic simplified)
if ! dotnet tool list --tool-manifest "$MANIFEST_FILE" | grep -q "swashbuckle.aspnetcore.cli"; then
    echo "Installing swashbuckle.aspnetcore.cli..."
    dotnet tool install Swashbuckle.AspNetCore.Cli --version 6.4.0 --tool-manifest "$MANIFEST_FILE"
else
    echo "Tool already installed, verifying version..."
    # We could check version here, but dotnet tool install matches version requirements or errors
    # To keep it simple, we just try to update or assume it's good. 
    # If we really want to enforce version, we can uninstall and reinstall, or restore.
    # Restore is idempotent if manifest matches.
    dotnet tool restore --tool-manifest "$MANIFEST_FILE"
fi

# Publish the API project
echo "Publishing API project..."
PUBLISH_DIR="$PROJECT_ROOT/Kr.__PROJECT_NAME__.Api/bin/Release/publish"
dotnet publish "$API_PROJECT_PATH" --configuration Release --output "$PUBLISH_DIR"

# Check if appsettings.json exists in publish dir, if not copy it
if [ ! -f "$PUBLISH_DIR/appsettings.json" ]; then
    echo "Copying appsettings.json to publish directory..."
    cp "$PROJECT_ROOT/Kr.__PROJECT_NAME__.Api/appsettings.json" "$PUBLISH_DIR/"
fi

# Copy the tool manifest to the publish directory so 'dotnet swagger' can find it
mkdir -p "$PUBLISH_DIR/.config"
cp "$MANIFEST_FILE" "$PUBLISH_DIR/.config/"

# Change to publish directory to run the tool
cd "$PUBLISH_DIR" || exit

DLL_NAME="Kr.__PROJECT_NAME__.Api.dll"

if [ ! -f "$DLL_NAME" ]; then
    echo "Error: Could not find published DLL at $PUBLISH_DIR/$DLL_NAME"
    exit 1
fi

echo "Using DLL: $DLL_NAME"

# Generate OpenAPI Spec (Swagger)
echo "Generating OpenAPI Specification..."
# Using 'dotnet tool run swagger' which looks for manifest in current dir (.config/dotnet-tools.json)
dotnet tool run swagger tofile --output "$OUTPUT_FILE" --yaml "$DLL_NAME" v1

STATUS=$?

# Cleanup (optional)
rm -rf "$PUBLISH_DIR/.config"

if [ $STATUS -eq 0 ]; then
    echo "OpenAPI spec generated successfully at $OUTPUT_FILE"
else
    echo "Failed to generate OpenAPI spec."
    exit 1
fi
