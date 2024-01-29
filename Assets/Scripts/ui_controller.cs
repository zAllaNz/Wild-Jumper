using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_controller : MonoBehaviour
{
    public player_controller var;

    // Variáveis de pontuação
    public float distancia;
    public Text distancia_text;
    public int coin;
    public Text text_coin;

    // Variáveis de UI
    public GameObject hearth;
    public Canvas canvas;

    // Variáveis do Mindwave
    public Text attencion;
    public Text meditation;
    private mind_wave mw_var;
    private bool mind_on = false;

    // Start is called before the first frame update
    void Start()
    {
        var = GameObject.FindGameObjectWithTag("Player").GetComponent<player_controller>();
        // Instanciando a imagem hearth na UI
        for (int i = 2; i <= var.vida; i++) 
        {
            GameObject hearth_instance = Instantiate(hearth, canvas.transform);
            hearth_instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(-33 * i,-30);
            hearth_instance.name = "hearth"+i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Aumentando a pontuação se o player estiver vivo
        if(!var.player_dead){
            distancia += Time.deltaTime * (var.vel);
            distancia_text.text = Mathf.Round(distancia).ToString()+"m";
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            mw_var = GameObject.FindWithTag("Mind").GetComponent<mind_wave>();
            mind_on = true;
        }

        // Adicionando ou removendo a indiciação de vida para o player
        if(var.vida_add)
        {
            var.vida++;
            GameObject hearth_instance = Instantiate(hearth, canvas.transform);
            hearth_instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(-33 * var.vida,-30);
            hearth_instance.name = "hearth"+var.vida.ToString();
            var.vida_add = false;
        }
        if(var.vida_remove)
        {
            var.vida_remove = false;
            Destroy(GameObject.Find("hearth"+var.vida.ToString()));
            var.vida--;
        }

        // Adicionando as variáveis do mindwave na tela
        if (mind_on)
        {
            attencion.text = mw_var.Attention.ToString();
            meditation.text = mw_var.Meditation.ToString();
        }
    }

    public void add_coin()
    {
        coin++;
        text_coin.text = coin.ToString();
    }
}
