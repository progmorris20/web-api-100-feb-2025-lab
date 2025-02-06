# Lab Suggestion


Like we did yesterday with vendors, we need a resource to keep track of the Software Center API Techs.

The software center manager is the only one allowed to add techs.

Each tech should have a name (a simple string is fine), an email address, and the manager should provide the `sub` claim for the tech.

When added, each tech should have:

1. An ID (Guid) assigned to them. We will *never* display their `sub` claim through the API.
2. The `sub` claim of the software center manager that added them. (this should be stored, but not returned from the API, it is for internal tracking).
3. An email address or a phone number (they must have one or the other).

Create an endpoint that allows us to get a list of all the techs and their email address or phone number.

## Suggestions

You can do this in the current project, or you can create a new API project in your `src` directory. Your choice.

