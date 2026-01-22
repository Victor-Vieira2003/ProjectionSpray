using System;
using System.Collections.Generic;
using UnityEngine;

public class Caneta : MonoBehaviour
{
    #region Variaveis

    [Header("Propriedades da Caneta")] public Transform ponta;
    public Material materialDesenho;
    public Material materialPonta;
    [Range(0.01f, 0.1f)] public float larguraDaCaneta = 0.01f;
    public Color[] colorDaCaneta;

    [Header("Maos e Agarrao")] public OVRGrabber rightHand;
    public OVRGrabber leftHand;
    public OVRGrabbable grabable;

    private LineRenderer gravuraAtual;
    private List<Vector3> posicaoDaCaneta = new();
    int index = 0;
    private int corAtualIndex;

    #endregion

    #region Metodos Padrao

    private void Start()
    {
        corAtualIndex = 0;
        materialPonta.color = colorDaCaneta[corAtualIndex];
    }

    private void Update()
    {
        bool agarrado = grabable.isGrabbed;
        bool desenhandoDireita = agarrado && grabable.grabbedBy == rightHand &&
                                 OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        bool desenhandoEsquerda = agarrado && grabable.grabbedBy == leftHand &&
                                  OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);

        if (desenhandoEsquerda || desenhandoDireita)
        {
            Draw();
        }
        else if (gravuraAtual != null)
        {
            gravuraAtual = null;
        }
        else if (OVRInput.GetDown(OVRInput.Button.One))
        {
            TrocarCores();
        }
    }

    #endregion

    #region Metodos

        private void Draw()
        {
            if (gravuraAtual == null)
            {
                index = 0;
                gravuraAtual = new GameObject().AddComponent<LineRenderer>();
                gravuraAtual.material = materialDesenho;
                gravuraAtual.startColor = gravuraAtual.endColor = colorDaCaneta[corAtualIndex];
                gravuraAtual.startWidth = gravuraAtual.endWidth = larguraDaCaneta;
                gravuraAtual.positionCount = 1;
                gravuraAtual.SetPosition(0, ponta.transform.position);
            }
            else
            {
                var pos = gravuraAtual.GetPosition(corAtualIndex);
                if (Vector3.Distance(pos, ponta.transform.position) <= 0.01f)
                {
                    index++;
                    gravuraAtual.positionCount = index + 1;
                    gravuraAtual.SetPosition(index, ponta.transform.position);
                }
            }
        }

        private void TrocarCores()
        {
            if (corAtualIndex == colorDaCaneta.Length - 1)
            {
                corAtualIndex = 0;
                
            }
            else
            {
                corAtualIndex++;
            }
            
            materialPonta.color = colorDaCaneta[corAtualIndex];
        }

    #endregion

    //teste
}
