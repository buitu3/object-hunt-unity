<h1 align="center">
  Editing Map Tool Guide
</h1>

# Creating new map
- First you need to open the MapTemplateScene in the project <br>

![open maptemplate scene](/../main/Screenshot/Screenshot_createmap_01.png)

- Then on the Hierachy tab, click on the `MapEditor` gameobject and on the `MapCreator` script enter the information of the map <br>
  - `Map name` and `Map preview` image is what will be showed in the level select scene
  - The number of smaller areas will be in the map <br>
  - How many columns of the grid that will contain areas (Ex: 4 areas and 2 columns means that the areas will be organized in a grid that have 2 rows and 2 columns)
  - Each item in the `MapAreaBG` list is a collection that store all the background sprites of area (Ex: all background sprites of the first area will be put in the first list)
  - After all datas are set, click on the `Create Map` button <br>

![open maptemplate scene](/../main/Screenshot/Screenshot_createmap_02.png)

- A new map will be created based on the input datas <br>

![new map](/../main/Screenshot/Screenshot_createmap_03.png)

- Now to add hidden objects into the new map, we continue with the `Map Editor` script below
  - The `Current Map Object Data` will store all types of hidden object that will be used in the map, to add item to this collection we click on the `Add new item` button <br>

![popup](/../main/Screenshot/Screenshot_editmap_01.png)

- Then a popup is showed, we click on the `Select GameObject` button

![select gameobject](/../main/Screenshot/Screenshot_editmap_02.png)

- Then we choose the object we want to add on the Select GameObject popup <br>

![remove object](/../main/Screenshot/Screenshot_editmap_03.png)

- You can see newly added object is showed on the editor, in case you accidently added wrong object, you can remove it from list by put the object ID in the field `Remove Object ID` and then click `Remove Item` <br>

![adding object dict](/../main/Screenshot/Screenshot_editmap_04.png)

- Now after we done setting the list of hidden objects that will be used in map, we can move onto adding them to areas <br>

![click button](/../main/Screenshot/Screenshot_editmap_05.png)

- To add the object you want into an area, you click onto the `Add` button next to the item you want to add in the field represent that area <br>

![remove object](/../main/Screenshot/Screenshot_editmap_06.png)

- You can see that a new object of that type is instantiated inside the map prefab, to remove the object from map you can click on the `x` icon next to the item on the editor <br>

![save changes](/../main/Screenshot/Screenshot_editmap_07.png)

- After all objects are added and moved to the right position, you can save your changes by clicking the `Save changes` button, it will apply all your changes into the map prefab <br>

![import map](/../main/Screenshot/Screenshot_importmap_01.png)

- For maps that you already created before, you can also import their data and makes changes to the map just like when you create maps


