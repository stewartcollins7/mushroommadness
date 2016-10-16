Graphics and Interaction
Assigment 2 Report
Stewart Collins - 326206

What the application does

The windows store app I developed is an arcade game centred around picking mushrooms. The aim of the
game is to try and achieve the highest score and beat previous scores for each level. The gameplay
is set in one arena which is a grassy field with trees, hills and a river. The user is given a
particular type of mushroom that is the goal for that level, and they have to try and pick as
many mushrooms of that variety as they can. There is a time limit of 60 seconds for the level, as well
as a hunger bar that is decreasing throughout gameplay. The hunger bar will deplete before the time
limit, so in addition to trying to save as many mushrooms as possible the user must also balance
the need to replenish the hunger bar. 420 points are awarded for collecting a mushroom of the correct
type, and 100 points are removed for collecting a mushroom of the incorrect type. Points are also
awarded for removing grass and leaves as will be explained later.

The game is further complicated by the fact that the mushrooms are sometimes obscured by either leaves
or grass. The user is able to remove these obstacles to see if there are any mushrooms behind them. The
user also experiences different effects from eating mushrooms. The button mushrooms are edible, and
eating them will provide a boost to the hunger bar which will allow them more time to continue picking.
The death cap mushrooms are poisonous and will reduce the hunger bar further to indicate throwing
up after eating them, and so generally these should be avoided. The third type of mushroom is
hallucinogenic. While eating these mushrooms will reduce the rate at which the user gets hungry, this
will also effect the view of the camera and make it more difficult to spot and identify mushrooms. 
In this case eating a poisonous mushroom will actually help the user removing the effects of the
hallucinogenic mushrooms, again meant to indicate throwing up the mushrooms. In this instance the
hunger bar is not reduced by throwing up.

After completion of the level the game will check to see if the new high score is higher than any
of the 5 previously stored high scores on the local data of the device. If this is the case then
the user will be prompted to enter their initials, and then this new data will be saved on the
device. The user is then given a rundown of their score for the level, including how many correct
mushrooms were picked, how many incorrect were picked, and the number of leaves and grass clumps
taken away.

How to use it

The interface for the application is primarily touch based, and all interactions with the menu screens
are handled through touch control. The user interface for the menu screens is fairly straightforward, 
with the user utilising buttons to interact with the menu. These buttons take a familiar form to those 
found in other mobile applications, to allow the user to naturally work out how to interact with the menu
and they also highlight when pressed to indicate response to user input. An instructions screen in the
menu provides more detailed instructions on how to interact in game, however these controls have been
designed in such a way that the user should hopefully be able to intuit or discover these controls by
themselves.

In game the user can interact with the mushrooms, the grass and the leaves. The user must swipe left or
right to brush away leaves, swipe up to pluck grass, and tap on a mushroom to inspect a mushroom. Once
a mushroom has been tapped it moves closer to the camera so the user can inspect it before deciding
whether to eat it, keep it, or throw it away. While a mushroom is being inspected no other objects can
be touched. The user may rotate the mushroom while inspecting it by tapping on the screen and dragging
left or right, or up or down. When the user has made a decision they use the accelerometer to indicate
what they want to do with the mushroom. Tilting left siginfies keeping the mushroom tilting right to throw
it away, and tilting towards indicates eating, which was chosen as the action resembles putting something
to your mouth.

How objects and entities were modelled

The three mushroom models were constructed from basic shapes using blender. A cylinder was used as the 
base shape, and then various transformations were applied such as extruding regions, loop cut and
slide, and scaling, shrinking and translate various verticies and edges. After the models were constructed
they were colored in blender using the vertex paint method. This allows you to color individual verticies
which was perfect for implementing with the phong-bling illumination model we had been taught, that relied
on vertex color interpolation, however it did have it's limitations in the sense that because the models
were reasonably low detail, despite having probably 100 verticies, it made mushroom attributes like
spots hard to draw with clearly defined edges.

The terrain object that served as the game arena was constructed in Unity, which allows you to lower
and raise the height of a terrain using graphic tools. A texture for grass was found and applied to the
majority of the terrain. An area of the ground was lowered to simulate a river bed and a different earth
coloured texture was used for the rived bed. The river is simply a plane that had a water texture applied
to it that had a transparent quality. A script was used to change the offset of the river texture as the frames
progressed to give the impression of a moving river. All the textures used were found in free assets on the
unity store. The models for the trees, grass and the rock were also found in free asset packages on the
unity store. The sun was constructed from a simple unity sphere object.

How graphics and camera motion were handled

Two custom shaders were written. One utilises the standard bling-phong illumination model taught in
the subject workshops. This was applied to the both the leaves and grass clump objects. One adjustment
was made to the shaders to pass in a changing alpha value. This allowed the grass and leaves to gradually
fade out before being destroyed when they were swiped by the user. The other custom shader was also
based on the bling phong illumination model, however this utilised cel shading, and this was applied to
the mushroom models. Each mushroom had 8 color attributes that could be assigned in the unity gui, which 
were then passed to the shader when the level was loaded. These represented the available color palette
of the model. Once the shader had calculated the color of a fragment based on the interpolation of vertex
colors, it would check to see which of the 8 colors in the color palette was closed to the calculated color.
It would then apply that color, strongly limiting the available color spectrum of the model. This appeared
to be quite buggy in the final project, and a lot of fiddling with the color palette for the different
mushrooms was done to try and improve this. I think a lot of the issues were down to my inability to select
an appropriate color palette rather than the implementation of the shader although I am not completely sure about
this, but graphic design in general is not my forte.

Shadows were produced using the in built unity shadow system. As the phong shaders used a manually
set point light system represented by a sun, a unity point light was also attatched to this object to
ensure there was no difference in the direction of the different light sources. The trees and rock then cast
shadows on the terrain. A particle system was applied to the rock in the water to simulate spray off
the rock from the fast moving river. This also utilised unity's in built particle system. While the
camera is stationary during game play, before play begins the camera is moved around the game area to
give the user a sense of the space. This was acomplished by setting way points for the camera with a
set world position and rotation, and then using the slerp function to gradually move between these
positions. The hallucinogenic effects from eating mushrooms were implemented by using the ImageEffects
component of the unity Standard Assets Package, which allowed for some adjustable variables such as
intensity of blurring that were appropriately chosen to achieve the desired effect.

Code used that was not your own

I did not use any code or downloaded APIs to develop this game outside of the Image Effects BloomAndFlare
code from the standard assets package used to visualise the effects of hallucinogenic mushrooms. The
phong-bling shader code is heavily based on the code provided in the labs but adjustments were made to
suit the desired application. Several of the models used were sourced from the asset store as noted above
and some of the images used in game were sourced from public domain image websites and are copyright free, 
as is the font that was used in the game.



