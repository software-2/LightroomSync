# LightroomSync
 
A tool to manage your Lightroom Classic catalogs across multiple Windows computers.

Lightroom catalogs are designed to be single-user, and Lightroom won't even let you run your catalog from a network folder, even though all your photos are on that NAS. But if you're like me, you want to work on your photos in multiple locations (such as your laptop on the couch for triaging, and your main workstation for more advanced editing). This means having to manually copy your catalog around, and that's a pain. Well no more! This program will save your catalog(s) to a network folder, and automatically update your local catalogs if a newer version is on the network. 

![Screenshot](https://github.com/software-2/LightroomSync/blob/master/Screenshot.png?raw=true)

## Features
- Automatically uploads a zip of your catalog to your network folder as soon as you close Lightroom.
- Automatically replaces your catalogs with the newest version on the network.
- Prevents you from having Lightroom open on multiple machines at once to avoid conflicts and overwriting work.
- Can launch at startup in the system tray - never think about this problem again!

## Setup Instructions
- Build from source, or use the latest installer on the [Releases](https://github.com/software-2/LightroomSync/releases) page.
- On each machine, specify the directory where all your catalogs are stored locally.
- Specify a common network location that all machines can access. This is where catalogs will be stored.
- If you want this to run all the time, select File > Run At Startup. It will silently run in the system tray.
- With this program running, open Lightroom Classic, then close it. After detecting Lightroom has closed, it will zip and upload your catalogs.
- Launch this program on another machine. It will grab every catalog from the network.

## Troubleshooting

### My catalog is missing! Oh my God!

If something went wrong, please [file a bug report](https://github.com/software-2/LightroomSync/issues) so I can try to fix it. But worry not, your catalogs should still be available on the network share, and a copy is also stored in %AppData% until after the zip is finished extracting. The zips in the network share are never deleted by this program as a safety measure. (And as a better backup solution than Adobe's.)

### The program crashed or isn't working.

It works on my machine! That means I need you to file a bug report here on GitHub. In the app there's an event textbox. Please copy/paste that into your bug report, as it will likely give me a better idea of what's going on.

### Where is the Mac version!

I don't use my Mac as anything more than a glorified build server. This program uses tons of Windows-specific calls, so it'd likely need to be rewritten. The config file is simple JSON though, so if you want to make a version that's compatible with this, it could certainly be done. If you want to work on that, reach out to me and I'll try to help.