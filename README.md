## Commands
Players must have the required permission and RA access to run the command. Arguments without brackets [] are a literal string.
| Command                     | Arguments                | Description                                                                                                                                                           | Permission           |
|-----------------------------|--------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------|
| blackoutzone / bz           | [zone] [time]            | Blacks out all of the lights in the given zone (doesn't close doors) for the specified time.                                                                          | fctrl.zones          |
| blastdoors / blastd         |                          | Forces the blast doors above Gate A and Gate B to close (doesn't start the nuke) (doors CANNOT be reopened)                                                           | fctrl.zones          |
| closezone / cz              | [zone]                   | Closes all of the doors in the given zone (doesn't lock them)                                                                                                         | fctrl.zones          |
| destroydoor / dd            | add/remove [player]      | Gives/Removes destroy door mode from the targetted player(s) (every door they have permission to open will be destroyed upon interaction)                             | fctrl.destroydoor    |
| escaping / esc              | on/off                   | Toggles escaping. Note: Each player can only escape once, turning it off and back on will not allow players who passed the escape to escape.                          | fctrl.settings       |
| forcerespawn / fre          | chaos/mtf                | Same as built in respawn command, however this command also spawns vehicles and waits for them to get to the proper position before spawning.                         | fctrl.respawn        |
| isolate                     | [zone]/facility [time]   | Will lock all access points (gates, checkpoints, etc) to the specified zone for the specified time. Specify "facility" to lock gates A and B only.                    | fctrl.zones          |
| killclasszone / kcz         | [class #] [zone]/surface | Instantly kills all players of a specified class in the specified zone.                                                                                               | fctrl.killzone       |
| killzone / kz               | [zone]/surface           | Instantly kills all players in the specified zone.                                                                                                                    | fctrl.killzone       |
| lockzone / lz               | [zone] [time]            | Locks all doors in the given zone for the specified time.                                                                                                             | fctrl.zones          |
| nointeractdoors / nid       | add/remove [player]      | Gives/Removes NID mode from the targetted player(s). Players with NID mode cannot open/close ANY door (regardless of keycards).                                       | fctrl.nointeractdoor |
| nukeelevator / nelev        | on/off                   | If turned off, the nuke elevator will be disabled and cannot be used (prevent turning off the warhead). Players in the nuke room will be stuck. Turn on to re-enable. | fctrl.elevators      |
| prygates                    | add/remove [player]      | Gives/Removes the ability for the targeted player(s) to pry all gates.                                                                                                | fctrl.prygates       |
| startdecontamination / sdec |                          | Force starts light containment zone decontamination.                                                                                                                  | fctrl.sdecon         |
| toggleteslas / tt           | on/off                   | If turned off, tesla gates will be disabled. Turn on to re-enable.                                                                                                    | fctrl.teslas         |

## Valid Zones
- `light` - Light Containment
- `heavy` - Heavy Containment (plus the room connecting heavy and entrance)
- `entrance` - Entrance Zone
- Surface CANNOT be used unless it is specified in the command's arguments.

## Valid Player Formats
- `*` - All players in the server
- A single number - A single player's user ID (eg. `3`)
- Multiple numbers separated by `.` - Multiple players' user IDs (eg. `2.3.4`)
- `%N` - All users of role `N` (see role list below) (eg. `%1` to target all Class-D personnel)
- `*zone` - All players in the specified zone (see zone list above) (use `surface` to target players on surface) (eg. `*light` to target all players in light containment)

## Valid Roles
- `0` - SCP-173
- `1` - Class-D Personnel
- `2` - Spectator
- `3` - SCP-106
- `4` - Nine-Tailed Fox Scientist
- `5` - SCP-049
- `6` - Scientist
- `7` - SCP-079
- `8` - Chaos Insurgency
- `9` - SCP-096
- `10` - SCP-049-2
- `11` - Nine-Tailed Fox Lieutenant
- `12` - Nine-Tailed Fox Commander
- `13` - Nine-Tailed Fox Cadet
- `14` - Tutorial
- `15` - Facility Guard
- `16` - SCP-939-53
- `17` - SCP-939-89