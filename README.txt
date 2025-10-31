Here are the instructions on how to run the project:

1. Install the unity version: 6000.0.54f1
2. Create a new project within unity (ideally call the project the same as our project, just in case of any conflicts/problems)
3. download the zipped code from github and extract it.
4. copy the extracted folder to the "GamesProgramming_Assignment2" folder, if asked just replace any already existing files.
5. now our project should appear and be playable.

Controls:
Level 1 - Space to Jump
Level 2 & 3 - A, D, (left and right movement respectively), Space to Jump.

Contributions:
Aditya Suthar - 
Hayden Janecic - 
Jack Alonte - Improvements on Level 2 (Desert level), Audio additions, UI improvements 
Justin Hardie - Level 3 (Forest level), 3 Physics Materials (Slippery, Grippy, Trampoline), HUD (Hearts and Bag counter functionality).

Part 1 feedback addressed and what I worked on (JACK ALONTE):
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

GAMEPLAY AND LEVEL DESIGN
- The level's cave section was improved to make it stand out more by adding rock textures. (Relates to: Levels & Progression — “Refine level environments for clarity and pacing.”)
- To clarify the SDG theme and the level's objectives, an introduction was added as well as some concluding text when the player wins. (Relates to: Thematic Focus — “Add reflections or narration to reinforce the educational message.”)

*end of Jack's section*


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
