# Template App

This repository serves as an example of how to write plugins for the BlackBird platform.

<!-- begin docs -->

## Actions

### General

- **List berries** Outputs berries from the catalog.
- **Get berry** Finds a berry by name and outputs its details.
- **Download file by URL** Downloads a file from the supplied URL and outputs it for later steps.
- **Create callback** Registers a callback to be sent to the supplied URL.
  - **Action** — The action to associate with the callback.
- **Dynamic input with parameters** Uses the Entity ID and URL to populate the Dynamic input options and outputs the supplied values.

### Files

- **Download file** Downloads a file from the supplied URL.
- **Download large file** Downloads a large file from the supplied URL while keeping memory use low.
- **Upload file** Uploads a file to the supplied URL.

## Events

### Items

- **On item created** Triggers when an item is created and outputs its ID and title.
- **On project item created** Triggers when an item is created in the selected project and outputs its ID and title.

### Callbacks

- **On callback received** Triggers when a callback arrives and outputs its data.

### Berries

- **On polled event** Triggers when new berries appear in the catalog and outputs them.

## Connections

The template shows two connection types in [ConnectionDefinition.cs](TemplateApp/Connections/ConnectionDefinition.cs).

- **Credentials** — Enter an **API token** and select an **API version**.
- **OAuth** — Enter the service **URL** and authorize the connection.

The [OAuth connection services](TemplateApp/Connections/OAuth) show how to build the authorization URL and manage OAuth credentials.

<!-- end docs -->

See the [developer notes](TemplateApp/Readme.md) for the sample code layout.
