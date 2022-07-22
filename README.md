# Squadron Dump

A quick and dirty command line application to dump the members of your **Elite Dangerous** squadron to a CSV. You can use this to prune inactive members,
identify potential promotions or synchronize levels with other services like **Inara** or **Discord**.

# Use

1. Copy [`appSettings.sample`](SquadronDump/appSettings.sample) to `appSettings.json`.
2. Open https://www.elitedangerous.com/community/squadrons/browse in a browser.
3. The browser will redirect you to authenticate. Log in with the same credentials as you use in-game.
4. Once the page shows the correct results, enter Developer Mode, copy the "authorization" header value for any server request then paste it in the `token` value in `appSettings.json`. Remove the preceeding "Bearer".
5. Enter your four letter squadron tag into the `tag` value in `appSettings.json`.
6. Enter your platform into the `platform` value in `appSettings.json`, usually `PC`.
7. Save `appSettings.config` then run `SquadronDump.exe`. Pipe the output, e.g. `squadronDump > members.csv` to create a file you can open in a spreadsheet application.

# Errors

| HTTP Error | Description |
| --- | --- |
| 401 | The token pasted in `appSettings.json` in step 4 has expired or is invalid. Repeat steps 2 through 4 above. Remember to remove the preceeding "Bearer". |
| 403 | Players can only view members of their own squadron. Frontier imposes this presumably to protect players' privacy. |
| 404 | The squadron does not exist. Check the `tag` and `platform` values added steps 5 and 6 to `appSettings.json`. |


# License

See [LICENSE](LICENSE) for details (GNU Public License v3).
