using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    //usa o m�todo novo de input da unity

    public GunBase gunBase;
    public Transform gunPosition;

    public List<GunBase> gunList;

    private GunBase _currentGun;

    

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        //funciona via callbacks
        //verifica se a acao conforme o editor foi realizada e busca um callback

        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();


    }

    private void CreateGun()
    {
        //pedido pelo exerc�cio -> trocar arma usando 1 e 2
        //input acessa a lista e instancia um numero especifico dela
        //inputs.Gameplay.ChangeGun1.performed += ctx => Instantiate(gunList.Count, gunPosition)


        //jeito da aula, s� seleciona na m�o
        _currentGun = Instantiate(gunBase, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }


    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Shoot Start");

    }

    private void CancelShoot()
    {
        Debug.Log("Shoot Stop");
        _currentGun.StopShoot();
    }


}
