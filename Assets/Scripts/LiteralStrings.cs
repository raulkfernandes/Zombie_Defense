using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiteralStrings {

	// Scenes:
	public const string MainScene = "Cena Principal";
    public const string MenuScene = "Cena de Menu";

    // Tags:
    public const string Jogador = "Jogador";
    public const string Zumbi = "Zumbi";
    public const string ChefeDeFase = "ChefeDeFase";

    // Inputs:
    public const string Vertical = "Vertical";
	public const string Horizontal = "Horizontal";
	public const string Fire1 = "Fire1";

	// Animation Variables:
	public const string Atacando = "Atacando";
	public const string Movendo = "Movendo";
    public const string Morrendo = "Morrendo";

    // Interface Messages:
    public const string GameOver = "Você Sobreviveu Por:\n {0}min e {1}s";
	public const string TempoMaximo = "Tempo Máximo: {0}min e {1}s";
    public const string ZumbisMortos = "x {0}";

	//PlayerPrefs
	public const string TempoMaximoSalvo = "TempoMaximo";
    public const string NivelDeDificuldade = "NivelDeDificuldade";
}
