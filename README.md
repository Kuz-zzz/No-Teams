This simple TShock plugin prevents players from changing their team. 

## Usage
Download the latest version of NoTeams.dll from releases and copy it into your ServerPlugins folder.

## Permissions
``tshock.noteams`` - allows toggling NoTeams on and off and reloading the config.

## Commands
``/noteams on`` - turns NoTeams on;
``/noteams off`` - turns NoTeams off;
``/noteams reload`` - reloads the config.

## Config
``enabled`` - can be "on" or "off"; determines if NoTeams is on or off at the start (can still be toggled with commands even if set to "off"); 
``message`` - what player sees when he tries to switch teams;
``kickOnSwitchAttempt`` - determines if the player should be kicked when they try to switch teams.

