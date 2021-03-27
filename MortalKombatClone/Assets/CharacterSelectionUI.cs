using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : Singleton<CharacterSelectionUI> {

    public EventHandler OnPlayersSelected;

    public class CharacterSelection {
        public int Index { get; private set; }
        public int Character { get; set; }
        public bool Selected { get; set; }

        public CharacterSelection(int index) {
            Index = index;
        }
    }

    [SerializeField] private Transform[] charactersButtons;
    [SerializeField] private GameObject[] charactersGO;
    [SerializeField] private Transform[] charactersHolders;
    [SerializeField] private Transform[] selectionIndication;

    private readonly CharacterSelection player1 = new CharacterSelection(0);
    private readonly CharacterSelection player2 = new CharacterSelection(1);

    private CharacterSelection currentSelector;

    void Start() {
        currentSelector = player1;
        ChangeCharacter(0);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangeCharacter(0);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            ChangeCharacter(1);
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            SelectCharacter();
        }
    }

    private void ChangeCharacter(int characterIdx) {
        if(currentSelector == null) return;

        selectionIndication[currentSelector.Index].localPosition = charactersButtons[characterIdx].localPosition;

        if(charactersHolders[currentSelector.Index].childCount > 0) 
            Destroy(charactersHolders[currentSelector.Index].GetChild(0).gameObject);

        var newCharacter = Instantiate(
            charactersGO[characterIdx], 
            charactersHolders[currentSelector.Index]
        );
        newCharacter.SetActive(true);

        currentSelector.Character = characterIdx;
    }

    private void SelectCharacter() {
        if(currentSelector == null) return;

        selectionIndication[currentSelector.Index].GetComponent<Image>().color = Color.gray;
        currentSelector.Selected = true;
        charactersHolders[currentSelector.Index]
            .GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("selected");

        if(currentSelector == player1) { 
            charactersHolders[currentSelector.Index].Rotate(new Vector3(0, -60, 0));
        }
        if(currentSelector == player2) {
            charactersHolders[currentSelector.Index].Rotate(new Vector3(0, 60, 0));
        }

        if(player1.Selected && player2.Selected) {
            StartCoroutine(FightStart());
            return;
        }

        if(currentSelector == player1) {
            currentSelector = player2;
            selectionIndication[currentSelector.Index].gameObject.SetActive(true);
            ChangeCharacter(0);
        }
        else if(currentSelector == player2) currentSelector = null;
    }

    private IEnumerator FightStart() {
        charactersHolders[player1.Index]
            .GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("fightStart");
        charactersHolders[player2.Index]
            .GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("fightStart");

        yield return new WaitForSeconds(1f);

        OnPlayersSelected?.Invoke(this, EventArgs.Empty);
    }
}
