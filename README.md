# Introduction
Spotify Playlists Manager allows to see history of tracks in your Spotify playlists. It has features like:
 - Easly creating new playlist with all songs that was in it
 - Complete playlist history using favorite tracks
 - Looking through tracks with a simple list

# Getting Started
If you want to use this app, you have to do some things. First, you need to create an account in the [Spotify for Developers](https://developer.spotify.com/) site. After this in your dashboard you need to click "Create app" button. Then complete the form. Then in settings page copy your Client ID. Paste this into IDs.cfg file that is in your exe file. Add enter and do the same thing with Client Secret. It`s supposed to look like this:
```cfg
Your Client ID
Your Client Secret ID
```
Now you can having fun.

# Usage
The first time you start the app, the Spotify login page will be display. Next time you will be login automatically. Every time you will open app, it will update your playlist history, so before you decide to delete some tracks, first you should run app.

# Files Descriptions
File | Description |
| IDs.cfg | As you read earlier, it contains Spotify IDs |
| %appdata%/SPM/Cache/ | Cache files |
| %appdata%/SPM/playlists.txt | This file contains all tracks that was and is in playlists |

