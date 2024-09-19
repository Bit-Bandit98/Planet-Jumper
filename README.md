# Planet Jumper
 
A Cookie clicker inspired space exploration game that runs between player sessions, akin to a mobile game.

# Code Reflection
- Good seperation of classes, but could be split up a bit more.
- Too many unnecessarily big classes, including the "GM" script".
- Overeliance on the singleton pattern, which makes the classes that implement it tightly coupled.
- Code presentation is all over the place, too much indentation.
- Defining classes within classes is unecessary for the purpose of the project.
- Too much hardcoding leading to a difficult to extend code base (For example, "Explosion Colour" is a class that does what it says, but depends on hardcoding values, and will fail if not told what to do in an edge case.)
- Needs to be thought out a bit more to develop a system as complex as this.

Overall, an extremely thought provoking experience in the field of code organisation.
