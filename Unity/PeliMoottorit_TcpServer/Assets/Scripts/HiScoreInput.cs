/******************************************************************************
 * File        : HiScoreInput.cs
 * Version     : 0.1 
 * Author      : Toni Westerlund (toni.westerlund@lapinamk.com)
 * Copyright   : Lapland University of Applied Sciences
 * Licence     : MIT-Licence
 * 
 * Copyright (c) 2021 Lapland University of Applied Sciences
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HiScoreInput
/// </summary>
public class HiScoreInput : MonoBehaviour
{
    /// <summary>
    /// List of Letters
    /// </summary>
    [SerializeField]List<GameObject> listOfLetters = new List<GameObject>();

    /// <summary>
    /// Score Text
    /// </summary>
    [SerializeField] Text scoreText;

    /// <summary>
    /// score
    /// </summary>
    private float score;

    /// <summary>
    /// Score
    /// </summary>
    public float Score
    {
        set
        {
            this.score = value;
        }
    }

    /// <summary>
    /// Selected Letter
    /// </summary>
    private int selectedLetter = 0;

    /// <summary>
    /// Number of Letters
    /// </summary>
    private int numberOfLetters = 3;

    /// <summary>
    /// Default Color
    /// </summary>
    private Color defaultColor;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        selectedLetter = 0;
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;
        scoreText.text = score.ToString();


    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        defaultColor = listOfLetters[selectedLetter].GetComponent<Text>().color;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        // 6. Kutsu PrevLetter metodia kun vasennuoli näppäin on painettu
        if (Input.GetKeyDown(KeyCode.LeftArrow) || TcpServer.wRot >= 0.4)
        {
            PrevLetter();
        }


        // 7. Kutsu NextLetter metodia kun oikeanuoli näppäin on painettu
        if (Input.GetKeyDown(KeyCode.RightArrow) || TcpServer.wRot <= -0.4)
        {
            NextLetter();
        }


        // 8. Kutsu NextAlphaBet metodia kun Ylösnuoli näppäin on painettu
        if (Input.GetKeyDown(KeyCode.UpArrow) || TcpServer.yRot <= -0.4)
        {
            NextAlphaBet();
        }


        // 9. Kutsu PrevAlphaBet metodia kun Alasnuoli näppäin on painettu
        if (Input.GetKeyDown(KeyCode.DownArrow) || TcpServer.yRot >= 0.4)
        {
            PrevAlphaBet();
        }



        if (Input.GetKeyDown(KeyCode.Return)) {
            HiScore.Instance.Save(listOfLetters[0].GetComponent<Text>().text 
                + listOfLetters[1].GetComponent<Text>().text + 
                listOfLetters[2].GetComponent<Text>().text, score);

            //Lisaa tanne GameStop
            GameObject.Find("Text").GetComponent<MainMenu>().GameStop();

        }
    }

    /// <summary>
    /// NextAlphaBet
    /// </summary>
    void NextAlphaBet() {
        // 13. Valitulle kirjaimelle annetaan arvoksi seuraava kirjain aakkosista
        // Vinkki! Käytä char muuttujaa hyväksi ja ASCII koodeja, kirjaimina käytetään A-Z isot kirjaimet
        char kirjain = listOfLetters[selectedLetter].GetComponent<Text>().text[0];

        kirjain++;
        if (kirjain > 90) kirjain = (char)65;

        listOfLetters[selectedLetter].GetComponent<Text>().text = (kirjain).ToString();

    }


    /// <summary>
    /// PrevAlphaBet
    /// </summary>
    void PrevAlphaBet() {
        // 12. Valitulle kirjaimelle annetaan arvoksi edellinen kirjain aakkosista
        // Vinkki! Käytä char muuttujaa hyväksi ja ASCII koodeja, kirjaimina käytetään A-Z isot kirjaimet
        char kirjain = listOfLetters[selectedLetter].GetComponent<Text>().text[0];

        kirjain--;
        if (kirjain < 65) kirjain = (char)90;

        listOfLetters[selectedLetter].GetComponent<Text>().text = (kirjain).ToString();

    }


    /// <summary>
    /// NextLetter
    /// </summary>
    void NextLetter() {

        // 11.Valitaan seuraava kirjain muokattavaksi, jos nykyinen kirjain on viimeinen, siirretään "focus" ensimmäiseen kirjaimeen

        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = false;
        listOfLetters[selectedLetter].GetComponent<Text>().color = defaultColor;

        selectedLetter++; // numberOfLetters-1 voi korvata 2 jos muuttujaa tarvitaan
        if (selectedLetter > numberOfLetters-1) selectedLetter = 0;

        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;

    }

    /// <summary>
    /// PrevLetter
    /// </summary>
    void PrevLetter() {

        // 10. Valitaan edellinen kirjain muokattavaksi, jos nykyinen kirjain on ensimmäinen, siirretään "focus" viimeiseen kirjaimeen

        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = false;
        listOfLetters[selectedLetter].GetComponent<Text>().color = defaultColor;

        selectedLetter--;
        if (selectedLetter < 0) selectedLetter = numberOfLetters-1;

        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;

    }
}
