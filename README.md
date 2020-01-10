## **IERG3080** **Project2**  

### Baba is You

by HU, Han (1155107763) & HU, Zihao (1155107782)

---

#### Software Pattern Used

We use Model-view-presenter as our software design pattern.

1. **Model:** Corresponding to `class Instance, MapData, Element`.
2. **View:** Corresponding to `class MainWindow, Game, TextMode` and their corresponding GUI design.
3. **Presenter:** corresponding to `class Controller`.

---

#### Class Design

There are six important classes in this project, which are `Controller, Instance, Element, MapData, Game and TextMode`, respectively.

##### **Brief Introduction to the class design**

`MapData` is used to load the data of the elements from the pre-set maps for each level and passes the data to `Controller`.

Each object belongs to one type of instance. `class Instance` is created to handle the giving and change of the property. It also is responsible for stating the movements of other elements caused after a move of "You". 

For the objects with the same type of instance, the `class Element` provides methods managing the properties for all such objects.

`Class Controller` servers as the presenter of our project. It reads the map from the data from `MapData` and takes the user inputs from the view classes. After receiving the input from the user, it will cast a corresponding move and apply the game rules on the elements (which will be handled in `Instance`). Then, after the model part gives back the result of the movement, the presenter will update the view in the GUI correspondingly as well as update the game logic.

##### Details for each class

1. `class MapData`
  * Load the map resource files from local disk (normal mode)/predefined strings (text mode) and store the map of each level through a dictionary with level and data pairs.
  * Set the elements in the current map with corresponding instance type.
  * Store the positions of the objects.
2. `class Instance`
  * Define the properties of different kinds of objects.
  * Assign the instance type of each element.
  * Store previous objects and logic formed by "Is".
  * Determine how other objects move when "You"s move.
3.  `class Element`
  * Assemble the objects with the same instance type.
  * Get the instance and position of in-direction block objects.
4. `enum InstanceType` and `enum P_Type`
  * Classify the instance type of the objects
  * Distinguish the objects and the blocks
5. `class controller`
  * Switch mode: normal (graphical)/ text
  * Pass the loaded map data of current level to the view class (`class Game` or `class TextMode`) and control the view part to present the map and the objects.
  * React to the user’s input sent from the view class.
  * Contains most of the inner logic of the game, including:
    * Control the movement of the objects
    * Apply the game rules formed by "Is" sentences
    * Update the game rules according to "Is" sentences after each movement
    * Undo
    * Handling terminating and passing conditions
6. `class Game`
  * Support the GUI of the normal game mode
  * Handle the user's input through the keyboard
7. `class TextMode`
  * Support the GUI of the text game mode
  * Handle the user's input through user's button click

---

#### Event Response Mechanism

**Note:** *Since the text mode is well-defined in the project specification and it is controlled via the button click event, following the same game logic as the normal mode, the detailed event response mechanism of text mode will not be discussed in this report and the changes made for textmode will be discussed in the later section.*

In normal mode, the user can interact with the game through several keys. To be more specific, the user can control the movement of the entities with property "you" by pressing down one of the arrow keys, undo an action by pressing down 'Z' , restart the current level by pressing down 'R' and back to starting window by pressing down "ESC".

##### Arrow-Key Down Event

1. When an arrow key is pressed down, the Game class detects the event and record the corresponding move direction by storing indicator x or y.
2. After storing the pending movement, the instance of `class Game` (named as `GameWindow`) also stores the view part and calling the method in `class Controller` (named as `Controller`) to store the map data before the movement for the undo function.
3. Then, the event handler in `GameWindow` will call the methods in `class Controller`. Before moving the entities, the `Controller` will first check whether the entity has objects in its moving direction.
4. If yes, the `Controller` will pass the information into the `class Instance` to help determine the movement of other objects according to their properties. After that, the `Controller` will call the `move` method for all objects that need move. If no object is in the moving direction, the `Controller` will call the `move` method directly and move the entity.
5. The `Controller` iterates the above operations on all objects having the instance type of "You".
6. After the movement is done, the `Controller` will update the positioning information of each object and call the `GameWindow` to change the view according to the game progress.
7. The `Controller` then detects rule changes caused by the above movement by calling methods used to check the "Is" relationship to realize the properties update or entity transformation.
8. The `Controller` responds to the non-positional properties (SINK, KILL, MELT, WIN). The objects no longer involve in the game will be ~~removed~~ from the current data.
9. The `Controller` iterates the similar operation for "You" objects on all objects having property "MOVE".
10. The `Controller` repeats step 7 to update the game logic.
11. The `Controller` repeats step 8 to deal with redundancy elements.
12. If in step 8 or step 12, a "You" object is found at the same position on the map as a "WIN" object. `Victory` method is called to notify that the user wins the current level and load the map for the next level. If the current level, another event handler will take charge of this situation.

##### Winning Event

1. From step 12 in Arrow-Key Down event, if the current level is the final level, the `Controller` will pop up a message box to notify the end of the game. Meanwhile, it provides 2 options for the user to choose: Restart or Exit.
2. If the user chooses Restart, the `Controller` will clear all map data used in previous game and initialize the map again from level 1.
3. If the user chooses Exit, the `Controller` will call `Exit` method and close the game window as well as the starting window and exit immediately.

##### Undo Event

1. `GameWindow` detects the undo event and call the corresponding method `undo()` in `Controller`.
2. The `undo()` method will use the input information got from `GameWindow` to replace the current view model on the canvas with the nearest saved one.
3. Then the `undo()` method will dispose all newly created objects and replace the current data with the nearest saved one.
4. After updating the map data, the `Controller` calls the methods used to check "Is" relationships to update the overall game logic.

##### Restart Event

1. `GameWindow` detects the restart event and call the corresponding method in the `Controller.reload()`.
2. The `reload()` method will clear all data previously used in current level and initialize the map and the game logic of current level.

---

#### Class Reuse

`class MapData`:

This class loads the data from local files/predefined strings for each level's map and store it through a dictionary with level-mapdata pairs. Hence, there is no need for this class to have an instance. All methods in this class are static.

`class Instance`:

Instances of this class manage the instance type of the objects and assist the handling of corresponding object movements caused by the input of the user. Hence, for all objects having effect in the game, this class shall be reused. Also, the class is reused by the `Controller` after casting a move by the user.

`class Element`:

Similar to `class Instance`, reused by each instance type.

`class Controller`:

Though the class is called Controller, this class is more a combination of model functions controlling the game logic, updating the model properties and giving instructions to view change. Hence, the more proper name for this class is the Presenter. Since this class lies between the view part and the model part and interact with both parts, it has instances constructed both in `class Game/TextMode`, `class MapData` and `class Instance`.

`class Game`:

This is where the normal mode game lies. Only one instance of this class will be constructed.

`class TextMode`:

This is where the text mode game lies. Only one instance of this class will be constructed.

`class MainWindow`:

This is auto generated by the WPF template, contains the logic for the starting window. The instance of this class will be reused by `class Game` or `class TextMode` to deal with window navigation.

---

#### **Text Mode Modification (following the updated specification)**

Screenshot for the two modes:

<img src="\Screenshot_n.JPG" alt="Normal Version" style="zoom:75%;" /><img src="\Screenshot_t.JPG" alt="TextMode" style="zoom:75%;" />

As mentioned in the previous part and the update specification, the text mode uses a textbox to display the map and use button click handler to replace the keyboard input. 

In text mode, the map consists of several lines of strings. When a change occurred in the map, the string as a whole should be modified. Hence, the view part are greatly different from the normal mode. The information of each object in the text map defined in the static `class MapData` is transformed into the string by the mode-controlled method defined in `class Controller`. 

Similarly, the move part of the text mode varies greatly from the normal mode. Unlike the normal mode, each object binds to an image and the position setting is relatively easy to implement by set coordinates via the canvas, when a move event is detected by the `class TextMode`, we need to locate the positions of all the pending movements and the game iterates to update all the corresponding strings. 

Since in both modes, we classify all the objects through `class Instance` and store all the inner game logic, all methods related with the game logic are shared by the two game modes.

*However, due to the limitation of the textmode and the way that we implement the "BEST" game logic (overlay a blink image on the "BEST" objects), the "BEST" is not implemented in textmode.*

---

#### **Difficulties Overcome**

There are varieties of obstacles that we have come across during the whole designing, development and testing period of the project.

##### Understanding of the logic of the game itself is difficult

It’s kind of resembling the first phase of software engineering: understand the requirement of your clients. Without knowing the game logic or getting the view at the player side, we cannot implement the game properly. However, the game itself needs a bit of brain storming and made some difficulties for us to understand the game logic.

##### Dealing with the encapsulation

Although the encapsulation of C# is powerful to maintain the integrity of the code, it can sometimes get in the way and require you to create methods or classes to keep encapsulating the fields inside the class. We try to separate methods into according classes clearly with the classification defined in MVP scheme and make function calls simple in order to trace down the work flow.

##### Unfamiliar class properties and method usages

When it comes to operations in WPF window, we need to reference some class properties and method usage that we have not encountered yet (e.g. some methods in `TextBox class`, `Canvas class` and `List<> class`), which makes debugging more challenging. And when doing testing and debugging, we found that the method created by us failed due to minor mistakes.

##### Data structure of C#

During the implementation of our project, we use lots of `List<>` data type in the classes mentioned in the above sections. The lists that store the reference of the instances caused bugs when the program executes the undo, reset and updating logic function as previous data stored in the list may not be properly cleared or modified. Also, when storing the current list for further use (e.g. `undo`), using the simple assign method "=" would fail to meet the need as the assigned new list will also change with the old one. We avoid storing the list directly by extracting the needed data and store/clear them accordingly. 

##### Alignment of textmode

Though the specification told us that the textmode is not necessarily be pretty, but the alignment for this mode is of vital important to let users have a playable game. At first, we encounter some difficulties in making all the objects have the same display length as the uppercase letter, lowercase letter and white space may take different spaces in the textbox, which make the alignment be in a complete mess. We finally solve this problem by finding a monospaced font.

---

#### Remarks and Work Allocation

For your reference, we only copy two levels from the jam version of the game, which are just necessary for us to check whether the game logic and navigation functions are working properly.

We didn't use the GitHub repository the system offered in IECUHK. Instead, we use our own private repository for this project. So, the commit records are not accessible for you to check the work allocation.

HU, Han:

1. GUI Design and corresponding interaction methods
2. Minor game logic (e.g. Winning, Reload)
3. Insert the bgm (bought from commercial version DLC on Steam)
4. View model event handler
5. Program testing and bug detection
6. Resources I/O

HU, Zihao:

1. Major game logic (e.g. "Is" relationship updating function)
2. Inner move function (data side)
3. Map Design and finding resources
4. Textmode string design
