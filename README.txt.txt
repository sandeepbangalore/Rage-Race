Team Rocket 
1. Agneya A Kerure      akerure3        kerure.agneya@gatech.edu
2. Christina Chung      cchung44        cchung44@gatech.edu
3. Erin Hsu         ehsu7           ehsu7@gatech.edu
4. Dibyendu Mondal      dmondal6        dibyendu@gatech.edu
5. Sandeep Bangalore Venkatesh  sbv7            sandeepbangalore@gatech.edu

Changes from Previous Submission:
1. Previously, different characters would have different speeds when running the track. Because this was a mostly hidden feature and because we were unable to add other balancing/trade-off attributes to the characters, we have set the running speed to be the same across all player characters, regardless of model. Character selection is now a purely aesthetic and personal preference choice. We think this change is more fair to players and no longer forces them to make uninformed decisions when selecting a character.
2. We have added an auditory cue (crowd cheering/noise) when the player approaches parts of the track where the audience will toss slowing spheres at runners. This change was made to better alert players to their environment and to notify players when they might find themselves slowed soon.
3. We have fixed last minute bugs found in progress/position tracking near the end of the race and proper stopping of the timer. 
4. We have fixed some typos/formatting errors throughout the game for increased polish
5.We have noticed some more bugs (added to the Known Bugs section)

How to Play:
You can navigate the menu using the arrow keys and hit enter on Play to go to the PlayerSelectorMenu. You can select the player you want to play as in the game and click play, and you can select either the Day or Night mode to play in. This will load up the track that we will be using for the game. The game will count down from 3 to 1 and afterwards you can control your player with the arrow keys or WASD. As you pick up powerups, you can use left control and left alt to use them. Once the race finishes, it shows the ranking. You can go back to the main menu or quit the game.

Requirements:
1. 3D Game Feel: We have implemented a 3D game with an achievable goal which has a start and an end. You can restart or quit the game after you finish the race or bring up the pause menu.
2. Precursors to Fun Gameplay: The goal of the game is to win the race. Players can choose if and when to use power ups and avoid obstacles. While there are four distinct holdable power ups, the player can only hold two at a time and must decide which to pick up and when to use them. Players can also have their personal goals such as finishing the race under a time limit. 
3. 3D Character Control: We have a Mechanim-controlled player. We have a 2 smooth cameras. One which is at the start of the game, which revolves around the players and stops behind the player. The second camera follows the player during the game.
4. 3D World with Physics: The race is bounded with visible boundaries. We have graphically and auditorily represented physical interactions like picking up power ups. We have an interactive environment where the player has to avoid obstacles and the audience throws stones towards the players. 
5. Real Time NPCs: We have path-finding and root motion for humanoid characters. The AI characters also pickup and use power ups from the track. Their power up usage varies depending on the power ups they have; for example, any AI not in 1st place will use the homing missile power up if they have it, and an AI in front of the player will use slow gas/stun log if available. When an AI gets stuck (see known bugs section) behind the player, it teleports forward and gives itself a speed boost. Similarly, if an AI lags too far behind the player, it will give itself speed boost(s). 
6. Polish: We have a working main menu and pause menu. You can exit the game anytime by pressing the ESC key. We have background music during the race and rich sound feedback for various other interactions like weapons, pickups etc. The game has an overall unified aesthetic of a playful and silly world.

Known Bugs:
- AI can get turned around/"confused" if forced to turn by collision
- Sometimes the 3-2-1 countdown at the beginning of the race will occur more than once
- The homing missile will always target the first place runner, even if they have already crossed the finish line 
- To prevent the player from being stuck on top of the concrete walls of the track after being launched into the air, we moved invisible barriers closer to the track. Unfortunately, some barriers protrude slightly into the track and thus the player might find themselves colliding against seemingly nothing along the edges of the track.


External Resources:
EasyRoads3D by AndaSoft
Stonette6 Nightclub by Lip1996
Predictive Aim Mathematics for AI Targeting by Kain Shin
PowerUp Particles by MHLab
Audience Crowd by 8bull
Homing Missile in Unity by Jonathan Gonzalez
custommapmakers.org 
AwesomeNauts
Mixamo
Cartoon Nature Pack by Mooshoo Labs
freesound.com 


Who did what:
Agneya - Audio Implementation, UI Design, Main Menu, Environment
Christina - Powerup implementation, Gameplay video
Erin - AI, Position tracking and UI display
Dibyendu - Environment & Obstacle Design, Characters and animation, UI, Gameplay video
Sandeep - Player Selection, Track Design, Main Menu, Powerup Implementation, Environment, UI



Scenes (in order):
1. TeamLoadingScene
2. menu
3. PlayerSelector
4. TrackSelector
5. track

From the main menu one can view the credits, play game, view help, or quit. If you select play button then PlayerSelectorMenu is opened. You can select the player and mode you want to play as in the game and click play. This will load up the track that we will be using for the game.

