using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _characterStandPos;
    [SerializeField] private CharacterButton _characterButtonPrefab;
    [SerializeField] private CharacterDatabaseSO _characterDatabaseSO;
    private int _selectedIndex = 0;

    void Start()
    {
        PopulateCharacters(_characterDatabaseSO.GetTotalCharacters());
    }

    void OnEnable()
    {
        CharacterButton.OnCharacterSelected += SetSelectedCharacter;
    }

    private void SetSelectedCharacter(int index)
    {
        if (_selectedIndex == index)
            return;
        _characterDatabaseSO.GetCharacterSO(_selectedIndex).GetCharacter().gameObject.SetActive(false);
        _selectedIndex = index;
        _characterDatabaseSO.SetSelectedCharacter(index);
        ShowCharacter(_characterDatabaseSO.GetCharacterSO(index).GetCharacter());
    }

    void PopulateCharacters(int numberOfChars)
    {
        for (int i = 0; i < numberOfChars; i++)
        {
            Instantiate(_characterButtonPrefab, _container).Setup(i);
            _characterDatabaseSO.GetCharacterSO(i).SpawnCharacter().gameObject.SetActive(false);
        }

        // show the previous selected character;
        _selectedIndex = _characterDatabaseSO.GetSelectedIndex();
        ShowCharacter(_characterDatabaseSO.GetSelectedCharacterSO().GetCharacter());
    }

    private void ShowCharacter(Player character)
    {
        character.transform.position = _characterStandPos.position;
        character.transform.forward = Vector3.back;
        character.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        CharacterButton.OnCharacterSelected -= SetSelectedCharacter;
    }
}