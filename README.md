# Lightning Game Engine

## Code of Conduct (version 1.2) (April 3, 2021)

Welcome to the Lightning game engine main tree. We are going to have to ask you abide by a few rules if you wish to contribute code.

* Be respectful. This is the Prime Directive of development here and you have to follow it. Bigotry, unpleasantness, jackassery, and various other forms of unfortunately negative behaviours will not be tolerated and will result in you being removed. 

* This is a private repository and please treat it as such!

* All code you check into the main branch must compile. Do a private build to check that it compiles before committing it into the main tree!

* Please document all of the changes you make thoroughly using XMLDOC.

* Code reviews are a requirement for non-senior developers! Contact me or a senior developer for a code review.

## Style Guide
* Please add a class name for all Instance objects created.

* UpperCase is recommended.

* Please use the XML Serialiser for any and all code that deals with XML.

* .NET Core 3.1 and .NET 5.0 are the only required frameworks/ 

* ALL platform specific code MUST be wrapped in platform specific IFDEFS!

* All additions to the main engine feature set must concur with the DataModel model:
If it is not a user API and does not support a user API in a direct way, it may not be in the DataModel.
If it is a user-facing API, deals with scripting or does support a user API, it must be in the DataModel.

* Method names must be <componentname>_<purpose> for private internal methods.

### Version History
**V1.2 (April 3, 2021)**:


**V1.1 (March 7, 2021)**:
* Added a style guide for committing code to the main branch.

**V1.0 (March 6, 2021)**
* Added 