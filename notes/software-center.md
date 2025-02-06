# Software Center API

The Software Center "own" the list of approved software at our company.

## High Level

Members of the software center team can:

- Add new supported software to the catalog
- The member that added the item can edit the description of that item.

Managers of the software center team can:

- Approve and add new vendors to the list of supported vendors.
- Add new supported software to the catalog
- Retire software from the catalog
- Can edit any details of the item in the catalog.

Any Employee at our company can:

- See a list of currently supported software
- See details of a currently supported piece of software.


## Adding a New Item To The Catalog

A member of the Software Center team can make a request to our API to add a piece of software to the catalog.
They must provide:

- The Name of the Software (e.g. Visual Studio Code)
- The Vendor for that Software (e.g. Microsoft)
    - Note vendors must be approved and "created" by a software center manager.
- They must indicate if the item is "Free" or "Open Source", or requires a license.

After they submit the new item, if it is a valid request, it should appear as part of the software catalog.



```http
POST http://localhost:1337/catalog
Content-Type: application/json

{
  "name": "Visual Studio Code",
  "vendor": "Microsoft",
  "license": "OpenSource"
}
```

```http
400 BadRequest

{
    "message": "Sorry, bro, that didn't work",
    "details": {
        "vendor": "Vendor does not exist, have a manager add it"
    }
}
```
