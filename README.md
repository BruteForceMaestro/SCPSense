# SCPSense
 Tired of mute people on your team, when you don't know where they are, or if they are even alive? This is for you.
## Config
```
s_c_p_sense:
  is_enabled: true
  # SCPs see each other's HP and AHP
  see_h_p: true
  # SCPs can see the distance between them.
  see_distance: true
  # Path for the plugin file. Required to change for Linux users as %AppData% is a windows only feature.
  save_path: // exiled folder on windows.
```
## Commands 
Those commands are in the Client Console, executable by every user that joins the server.
These settings also get saved, so the user doesn't have to type them out every time they join the server.
```
.SCPSense (aliases: .ss) - parent command, must be typed before commands below.
size  (aliases: none) - size of the displayed text. default: 60
alignment (aliases: align) - alignment of the text. Refer to Unity Rich Text Tags for more information. default: left
```
