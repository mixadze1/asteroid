using TMPro;
using UnityEngine;

public class PlayerBackStep : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BackStep _backStep;
    [SerializeField] private TextMeshProUGUI _textBackStep;
    public const string FINISHING = "Finishing";

    void Update()
    {
        if (IsPositionFinishing())
        {
            if (_backStep.Enemy.IsDie)
                return;
            _textBackStep.gameObject.SetActive(true);
        }
        else
        {
            _textBackStep.gameObject.SetActive(false);
        }

        if (IsPositionFinishing() && Input.GetKeyDown(KeyCode.Space))
        {
            if (_backStep.Enemy.IsDie)
                return;
            Finishing();
            _backStep.Enemy.Finishing();
        }

            if (CheckEndAnimation())
        { 
            _textBackStep.gameObject.SetActive(false); 
            _playerMovement.IsFinishing = true; 
        }
            

            else
        {
            _playerMovement.IsFinishing = false;
        }
          
    }

    private bool CheckEndAnimation()
    {
        if (_playerMovement.Animator.GetCurrentAnimatorStateInfo(0).IsName(FINISHING))
        {
            GetMeleeWeapon();
            return true;
        }
        GetWeapon();
        return false;
    }

    private void GetMeleeWeapon()
    {
        _inventory.Weapon.gameObject.SetActive(false);
        _inventory.MeleeWeapon.gameObject.SetActive(true);
    }

    private void GetWeapon()
    {
        _inventory.Weapon.gameObject.SetActive(true);
        _inventory.MeleeWeapon.gameObject.SetActive(false);
    }

    private bool IsPositionFinishing()
    {
        if (_backStep.CanBackStep == true)
            return true;
        return false;
    }

    private void Finishing()
    {
        _playerMovement.Animator.Play(FINISHING);
       
        transform.position = _backStep.Enemy.transform.position - _backStep.Enemy.transform.forward * 1.5f;
    }
}