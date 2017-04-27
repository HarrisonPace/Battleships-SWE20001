using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The EndingGameController is responsible for managing the interactions at the end
/// of a game.
/// </summary>
static class EndingGameController {

	/// <summary>
	/// Draw the end of the game screen, shows the win/lose state
	/// </summary>
	public static void DrawEndOfGame() {


		if (GameController.Multiplayer) { //Multiplayer game

			if (GameController.HumanPlayerA.IsDestroyed) {
				UtilityFunctions.DrawField(GameController.HumanPlayerA.PlayerGrid, GameController.HumanPlayerA, true);
				UtilityFunctions.DrawSmallField(GameController.HumanPlayerB.PlayerGrid, GameController.HumanPlayerB);
				SwinGame.DrawTextLines("-- PLAYER B WINNER --", Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, 0, 250, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			} else {
				UtilityFunctions.DrawField(GameController.HumanPlayerB.PlayerGrid, GameController.HumanPlayerB, true);
				UtilityFunctions.DrawSmallField(GameController.HumanPlayerA.PlayerGrid, GameController.HumanPlayerA);
				SwinGame.DrawTextLines("-- PLAYER A WINNER --", Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, 0, 250, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			}

		} else { //Singleplayer game

			UtilityFunctions.DrawField(GameController.ComputerPlayer.PlayerGrid, GameController.ComputerPlayer, true);
			UtilityFunctions.DrawSmallField(GameController.HumanPlayerA.PlayerGrid, GameController.HumanPlayerA);

			if (GameController.HumanPlayerA.IsDestroyed) {
				SwinGame.DrawTextLines("YOU LOSE!", Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, 0, 250, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			} else {
				SwinGame.DrawTextLines("-- WINNER --", Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, 0, 250, SwinGame.ScreenWidth(), SwinGame.ScreenHeight());
			}

		} //end else Singleplayer game

	}

	/// <summary>
	/// Handle the input during the end of the game. Any interaction
	/// will result in it reading in the highsSwinGame.
	/// </summary>
	public static void HandleEndOfGameInput() {
		if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.vk_RETURN) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			if (GameController.Multiplayer) { //Multiplayer game

				if (GameController.HumanPlayerA.IsDestroyed) {
					HighScoreController.ReadHighScore(GameController.HumanPlayerB.Score);
				} else {
					HighScoreController.ReadHighScore(GameController.HumanPlayerA.Score);
				}

			} else { //Singleplayer game

				HighScoreController.ReadHighScore(GameController.HumanPlayerA.Score);

			} //end else Singleplayer game

			GameController.EndCurrentState();
		}
	}

}
