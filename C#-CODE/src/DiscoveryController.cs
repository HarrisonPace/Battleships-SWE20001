using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
static class DiscoveryController {

	private static bool _turnPlayerA = true;

	public static bool TurnPlayerA
	{
		get { return _turnPlayerA; }
		set { _turnPlayerA = value; }
	}

	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput() {

		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.AddNewState(GameState.ViewingGameMenu);
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			DoAttack();
		}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack() {

		Point2D mouse = default(Point2D);

		mouse = SwinGame.MousePosition();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32(Math.Floor((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
		col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

		if (GameController.Multiplayer) { //Multiplayer game
			if (TurnPlayerA) { //doing turn of playerA

				if (row >= 0 & row < GameController.HumanPlayerA.EnemyGrid.Height) {
					if (col >= 0 & col < GameController.HumanPlayerA.EnemyGrid.Width) {
						GameController.Attack(row, col);
					}
				}

			} else { //doing turn of playerB

				if (row >= 0 & row < GameController.HumanPlayerB.EnemyGrid.Height) {
					if (col >= 0 & col < GameController.HumanPlayerB.EnemyGrid.Width) {
						GameController.Attack(row, col);
					}
				}

			} //end else turn of playerB
		} else { //Singleplayer game

			if (row >= 0 & row < GameController.HumanPlayerA.EnemyGrid.Height) {
				if (col >= 0 & col < GameController.HumanPlayerA.EnemyGrid.Width) {
					GameController.Attack(row, col);
				}
			}

		} //end else Singleplayer game

	}

	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery() {

		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;


		if (GameController.Multiplayer) { //Multiplayer game
			if (TurnPlayerA) { //doing turn of playerA


				if (SwinGame.KeyDown(KeyCode.vk_c)) {
					UtilityFunctions.DrawField(GameController.HumanPlayerA.EnemyGrid, GameController.HumanPlayerB, true);
				} else {
					UtilityFunctions.DrawField(GameController.HumanPlayerA.EnemyGrid, GameController.HumanPlayerB, false);
				}

				UtilityFunctions.DrawSmallField(GameController.HumanPlayerA.PlayerGrid, GameController.HumanPlayerA);
				UtilityFunctions.DrawMessage();

				SwinGame.DrawText(GameController.HumanPlayerA.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
				SwinGame.DrawText(GameController.HumanPlayerA.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
				SwinGame.DrawText(GameController.HumanPlayerA.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);

				//debug
				SwinGame.FillRectangle(Color.White, 0, 0, 200, 15);
				SwinGame.DrawText("Turn: Player A" , Color.Black, 0, 0);


			} else { //doing turn of playerB


				if (SwinGame.KeyDown(KeyCode.vk_c)) {
					UtilityFunctions.DrawField(GameController.HumanPlayerB.EnemyGrid, GameController.HumanPlayerA, true);
				} else {
					UtilityFunctions.DrawField(GameController.HumanPlayerB.EnemyGrid, GameController.HumanPlayerA, false);
				}

				UtilityFunctions.DrawSmallField(GameController.HumanPlayerB.PlayerGrid, GameController.HumanPlayerB);
				UtilityFunctions.DrawMessage();

				SwinGame.DrawText(GameController.HumanPlayerB.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
				SwinGame.DrawText(GameController.HumanPlayerB.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
				SwinGame.DrawText(GameController.HumanPlayerB.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);

				//debug
				SwinGame.FillRectangle(Color.White, 0, 0, 200, 15);
				SwinGame.DrawText("Turn: Player B" , Color.Black, 0, 0);


			} //end else turn of playerB
		} else { //Singleplayer game


			if (SwinGame.KeyDown(KeyCode.vk_c)) {
				UtilityFunctions.DrawField(GameController.HumanPlayerA.EnemyGrid, GameController.ComputerPlayer, true);
			} else {
				UtilityFunctions.DrawField(GameController.HumanPlayerA.EnemyGrid, GameController.ComputerPlayer, false);
			}

			UtilityFunctions.DrawSmallField(GameController.HumanPlayerA.PlayerGrid, GameController.HumanPlayerA);
			UtilityFunctions.DrawMessage();

			SwinGame.DrawText(GameController.HumanPlayerA.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
			SwinGame.DrawText(GameController.HumanPlayerA.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
			SwinGame.DrawText(GameController.HumanPlayerA.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);

			//debug
			SwinGame.FillRectangle(Color.White, 0, 0, 200, 15);
			SwinGame.DrawText("Singleplayer game" , Color.Black, 0, 0);

		}



	}

}
