**Here are the instructions on how to run the project:

1. Install the unity version: 6000.0.54f1
2. Create a new project within unity (ideally call the project the same as our project, just in case of any conflicts/problems)
3. download the zipped code from github and extract it.
4. copy the extracted folder to the "GamesProgramming_Assignment2" folder, if asked just replace any already existing files.
5. now our project should appear and be playable.

**Controls:
Level 1 - Space to Jump
Level 2 & 3 - A, D, (left and right movement respectively), Space to Jump.
Level 4 - W, A, S, D and Space to Jump

**Contributions:
Aditya Suthar - Created the 3D Main Menu and 3D Level, Implemented the 3D character (Dyp the Penguin) from the Unity Asset Store, Developed the health system (hearts UI + damage feedback), Added camera follow system to track the player, Implemented spike obstacles with player damage and visual feedback, Created Win, Lose, and Pause screens with working logic, Integrated bag collection system for win condition, Used environment assets such as trees, fences, and rocks to enhance the 3D level’s design.
Hayden Janecic - 
Jack Alonte - Improvements on Level 2 (Desert level), Audio additions, UI improvements 
Justin Hardie - Added a Difficulty changer(Easy, Medium and Hard), added persistant storage of items collected within a JSON file, added a progress bar to each level that tracks the amount of bags collected, Updated Level 3 based on feedback, added functionality to level buttons (added the bag requirments to levels 2, 3 and 4/ updated code and buttons that transition between scenes), Created and wrote the "About" page.

**Part 1 feedback addressed and what I worked on (JACK ALONTE):
SOUND AND AUDIO
- Every scene now has background music. (Relates to: Audio & Immersion — “Use ambient or thematic audio to set the tone.”)
- Win and game over sounds have been added to the corresponding screens; background music is muted during these to avoid audio conflicts. (Relates to: Game Flow & Audio Clarity — “Ensure polished presentation and avoid overlapping sounds.”)
- Sound effects for item pickup have been added. (Relates to: Feedback, Animation & Assets — “Add audio feedback for interactions.”)
- When the character is hit, a player damage sound effect has been added. (Relates to: Physics & Modular Systems — “Add audio cues for collisions or damage.”)
- Added jump sound effect when the player jumps. (Relates to: Feedback, Animation & Assets — “Include audio feedback for interactions.”)
- Volume sliders were added to change the SFX and music levels. (Relates to: Usability & Accessibility — “Include an Options/Settings menu with audio controls.”)

VISUAL INPUT
- To show when damage is taken, a quick red damage flash was added to the player sprite. (Relates to: Feedback, Animation & Assets — “Add visual feedback for taking damage.”)
- To add depth and distinguish the foreground from the background, the sky background was given a parallax effect. (Relates to: Thematic Focus — “Use visual cues to set the tone and immerse the player.”)
- Reworked the oil sprite overlay to be more transparent and less obtrusive to the overall player experience. (Relates to: Innovation Features — “The oil sprite temporarily blocking the player's view is a creative concept but can negatively impact playability; consider reducing opacity or duration.”) 

GAMEPLAY AND LEVEL DESIGN
- The level's cave section was improved to make it stand out more by adding rock textures. (Relates to: Levels & Progression — “Refine level environments for clarity and pacing.”)
- To clarify the SDG theme and the level's objectives, an introduction was added as well as some concluding text when the player wins. (Relates to: Thematic Focus — “Add reflections or narration to reinforce the educational message.”)

*end of Jack's section*

**Justin's Section (Improvements made):
 - I added a difficulty selection system with Easy, Medium, and Hard modes. Each mode changes how much damage the player takes.
 - To make progress carry across levels, I implemented persistent item saving using a JSON file. Players can now keep their collected bags between levels, which ties into the overall progression and level unlocking system.
 - I also added a progress bar to each level, so players can clearly see how many bags they’ve collected out of the total needed.
 - I reworked Level 3 based on the feedback we received, improving its layout and pacing to make it more enjoyable and better aligned with the rest of the game.
 - I updated the level select menu to make the progression system more meaningful. Levels 2, 3, and 4 now unlock only when players have collected a set number of bags, and all buttons now work properly to transition between scenes.
 - I wrote the “About” page, explaining the game’s goals and how it connects to the SDG theme.

*End of Justin's section

**Aditya's section
Aditya worked on the 3D main menu which serves as the central navigation hub for the game, providing a fully 3D interface to access all levels and settings seamlessly.

Aditya was also the primary worker on the 3d level, which integrates the core gameplay feel and themes from the previous 2D levels. It incorporates W, A, S, D movement and Space to jump over spike obstacles and collect the same bag items. Win and loss states have carried over to ensure that the gameplay loop is still familiar to players.

*End of Aditya's section

*Hayden's section
Level Description:
This level (Level 1 - Ice Level) takes heavy inspiration from popular mobile games like jetpack joyride or subway surfers, where the player collects 12 recycling bags that spawn in continuously, while trying to dodge various obstacles. This connects to the SDG 15 theme by showcasing the melting polar ice caps as obstacles that endanger the player.

Improvements from assignment 2:
The HUD has been included in this level now, whereas prior there was nothing on the screen to indicate progress or any information to the player.
A splash animation has been placed behind the player utilize more animations and showcase when the player is on the ground
Object pooling has been implemented that when an obstacle reaches a specific -x value, they are disabled and reused at a later time.

Assets scene:
Simple 2d menu screen was created with text that references all assets utilized when making the game.
A penguin running across the screen also adds more animations to the game.

*End of Hayden's section

Innovation features:
Status-effect system driven by data - The Oil Status effect occurs when the player touches the oil barrel sprite in the scene. A brief oil overlay sprite will appear on the screen and has a timer/cooldown before it eventually disappears. All controlled via the OilBarrel and OilEffect scripts.
Procedural element - Level 1 uses procedural generation with item and obstacle spawning.


Third Party Assets:
Simple 2D Platformer BE2 Asset Pack (Unity Asset Store)

Free Sky Backgrounds, Author: Free Game Assets

Downtempo & Lofi & Progressive Music Pack Vol - 2 - Composed by One Man Symphony - https://onemansymphony.bandcamp.com/

SFX_Jump_09.wav by jalastram -- https://freesound.org/s/386620/ -- License: Creative Commons 0

SFX_Pickup_60.wav by jalastram -- https://freesound.org/s/386585/ -- License: Attribution 4.0

“Free Sky with Clouds Background Pixel Art Set” by CraftPix (craftpix.net)

WinSquare.wav by Fupicat -- https://freesound.org/s/527650/ -- License: Creative Commons 0

negative_beeps.wav by themusicalnomad -- https://freesound.org/s/253886/ -- License: Creative Commons 0

Hit_02.wav by LittleRobotSoundFactory -- https://freesound.org/s/270325/ -- License: Attribution 4.0
