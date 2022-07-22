# Squadron Dump

A quick and dirty command line application to dump the members of your **Elite Dangerous** squadron to a CSV file. You can use this file to prune inactive members, identify potential promotions/demotions or synchronize levels with other services like **Inara** or **Discord**.

# Use

These instructions assume you are comfortable using a browser's developer mode and editing a JSON file.

1. Copy [`appSettings.sample`](SquadronDump/appSettings.sample) to `appSettings.json`. `appSettings.json` is excluded from git to ensure security sensitive values are not accidentally committed.
2. Open https://www.elitedangerous.com/community/squadrons/browse in a browser.
3. Press the "Login" button on the top right.
4. The browser will redirect you to authenticate. Log in with the same credentials as you use in-game.
5. Once the page shows the correct results, enter developer mode, copy the "authorization" header value for any server request  then paste it in the `token` value in `appSettings.json`, removing the preceeding "Bearer ".
6. Enter your four letter squadron tag into the `tag` value in `appSettings.json`.
7. Enter your platform into the `platform` value in `appSettings.json`, usually `PC`.
8. Save `appSettings.json` then run `SquadronDump.exe`. Pipe the output, e.g. `squadronDump > members.csv`, to create a file you can open in a spreadsheet application.

# Errors

| HTTP Error | Description |
| --- | --- |
| 401 (Unauthorized) | The token pasted in `appSettings.json` in step 5 has expired or is invalid. Repeat steps 2 through 5 above. Remember to remove the preceeding "Bearer". |
| 403 (Access Denied)| Players can only view members of their own squadron. Frontier imposes this presumably to protect players' privacy. |
| 404 (Not found) | The squadron does not exist. Check the `tag` and `platform` values added steps 6 and 7 to `appSettings.json`. |


# License

See [LICENSE](LICENSE) for details (GNU Public License v3).
