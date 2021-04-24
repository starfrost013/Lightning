# Lightning Game Engine

## Code of Conduct & Style Guide (version 2.0) (April 24, 2021)

Welcome to the Lightning game engine main tree. We are going to have to ask you abide by a few rules if you wish to contribute code.

* Be respectful. This is the Prime Directive of development here and you have to follow it. Bigotry, unpleasantness, jackassery, and various other forms of toxic behaviour will not be tolerated and will result in you being removed. 

* This is a private repository and please treat it as such!

* All code you check into the main branch must compile at commit time; do a private build to check that it compiles before committing it into the main tree!

* Please document all of the changes you make thoroughly using XMLDOC.

* Code reviews are a requirement for non-senior developers! Contact me or a senior developer for a code review.

## Style Guide

* You must add a ClassName or all Instance objects created.

* PascalCase is recommended.

* Please use either DDMS (for DataModel-related content ONLY) or XML serialisation for any and all code that deals with XML.

* .NET Core 3.1 (soon to be .NET 5.0) are the only required frameworks. 

* ALL platform specific code MUST be wrapped in platform specific IFDEFS!

* All additions to the main engine API must be in accordance with the DataModel:

If it is not a user API and does not support a user API in a direct way, it may not be in the DataModel.

If it is a user-facing API, deals with scripting or does support a user API, it must be in the DataModel.

* Method names must be *componentname*_*purpose* for private internal methods or methods that use a .

* All code you write must not break the strict hierarchy principle of the DataModel!

* When adding an object to the DataModel, * **DO NOT** * use any IEnumerable objects, such as List<T>. Use the object's Children property, which is an `InstanceCollection`.

* When adding a file to the source tree, please place some descriptive comments at the start in order

```cs
	 /// <component class name>
	 ///
	 /// <date of creation> (<date of modification>)
	 ///
	 /// <brief description of purpose>```
	 
### Version History

**V2.0 (April 24, 2021)**
* Rewording
* Added commenting rules

**V1.6 (April 9, 2021)**
* Added a rule about IEnumerables in the DataModel.

**V1.5 (April 8, 2021)**
* Added a rule about DataModel hierarchy. A lot of rewording.

**V1.2 (April 3, 2021)**:
* Added clarification surrounding when code should and should not be in the DataModel and informatino surrounding method names.

**V1.1 (March 7, 2021)**:
* Added a style guide for committing code to the main branch.

**V1.0 (March 6, 2021)**
* Initial version