# Bible Trivia Challenge

Separate Unity 2022.3 project for an offline Bible trivia game.

## Current foundation

- Trivia question schema and 5,000 verified reference-based questions under `Assets/Resources/Trivia` (2,500 Old Testament and 2,500 New Testament).
- Runtime database loader with category and difficulty filtering.
- Editor validation command: `Bible Trivia > Validate Question Database`.
- Offline local progress saving for current score, best score, and completed questions.
- Configurable category and difficulty filters with randomized round order.
- Level progression with Easy, Medium, and Hard tiers and offline unlock persistence.
- Persistent visual theme with portrait-friendly background and readable control colors.
- Old Testament and New Testament category selection buttons.
- Explicit portrait layout sizing for the persistent 2D controls.
- Mobile safe-area support for notches and gesture regions.
- Optional answer sound effects with a saved Sound On/Off toggle.
- Achievement tracking and three-star performance ratings.
- One-use hints and deterministic offline daily challenge selection.
- Editor test coverage for question integrity and a scene-object validation command.
- Portrait build configuration and build-readiness validation commands.
- Original Scripture project remains separate and untouched.

## Verified playable flows

- Portrait game screen with responsive UI fitting the Unity Game window.
- Answer feedback, score/stars, next-question progression, and one-use hints.
- Saved scene objects, EventSystem input, database validation, and build-readiness checks.

## Unity setup

1. Open the project in Unity 2022.3.62f2.
2. Open `SampleScene`.
3. Run `Bible Trivia > Create Scene Game Objects` and save the scene.
   Use `Bible Trivia > Rebuild Scene Game Objects` if the scene was generated more than once.
4. Run `Bible Trivia > Validate Question Database` and `Bible Trivia > Validate Scene Objects`.
5. Run `Bible Trivia > Configure Portrait Build` before mobile testing.
