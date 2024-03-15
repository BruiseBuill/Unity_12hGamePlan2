using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spray
{
	public class GameControl : Single<GameControl>
	{
		public void Win()
		{
			if (GameManager.Instance().IsGame)
			{
                GameManager.Instance().IsGame = false;
                UIManager.Instance().OpenPanel("WinPanel");
                ObjectGenerator.Instance().Win();
            }
			
		}
		public void Lose()
		{
			if (GameManager.Instance().IsGame)
			{
                GameManager.Instance().IsGame = false;
                UIManager.Instance().OpenPanel("LosePanel");
            }
        }
	}
}