using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    private void Start() {
        CharacterSelectionUI.Instance.OnPlayersSelected += (sender, args) => {
            LoadFight();
        };
    }

    private void LoadFight() {
        Debug.Log("Load Fight");
    }
}
