#Squadron Dump

A quick and dirty command line application to dump the members of an **Elite Dangerous** squadron to a CSV. You can use this to prune inactive members,
identify potential promotions or synchronize levels with other services like **Inara** or **Discord**.

#Use

1. Copy `appSettings.sample` to `appSettings.json`.
2. Open "https://www.elitedangerous.com/community/squadrons/browse" in a browser.
3. The browser will redirect you to authenticate. Log in with the same credentials as you use in-game.
3. Once the page shows the correct results, enter Developer Mode and copy the "authorization" header value for any server request then paste it in the `token` value in `appSettings.config`.
4. Enter the four letter squadron tag you want members of into the `tag` value in `appSettings.json`.
5. Enter the platform you want members of into the `platform` value in `appSettings.json`.
6. Save `appSettings.config` then run `SquadronDump.exe`. Pipe the output, e.g. `squadronDump > members.csv` to create a file you can open in a spreadsheet application.