# Introduction
Spotify Playlists Manager allows to see history of tracks in your Spotify playlists.

# Technologies used
 - .NET 8
 - Windows Froms
 - [Spotify Web API](https://developer.spotify.com/documentation/web-api)

# Features
 - Easly creating new playlist with all songs that was in it
 - Complete playlist history using favorite tracks
 - Looking through tracks with a simple list

# Getting Started
To use this app you need .NET 8 Desktop Runtime. You can download this from [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or open an app (if you don't have installed runtime you will get a message with link to download). Second, you need to create an account in the [Spotify for Developers](https://developer.spotify.com/) site. After this, in your dashboard you need to click "Create app" button. Then complete the form. Then in settings page copy your Client ID. Paste this into IDs.cfg file that is in the same directory as your exe file. In new line do the same thing with Client Secret ID. It`s supposed to look like this:
```cfg
Your Client ID
Your Client Secret ID
```

# Usage
The first time you start the app, the Spotify login page will be display. Next time you will be login automatically. Every time you will open app, it will update your playlist history, so before you decide to delete some tracks, first you should run app.

# License
Distributed under the MIT License. See LICENSE.txt for more information.

# Files Descriptions
| File | Description |
| --- | --- |
| IDs.cfg | As you read earlier, it contains Spotify IDs |
| %appdata%/SPM/Cache/ | Cache files |
| %appdata%/SPM/playlists.txt | This file contains all tracks that was and is in playlists |

