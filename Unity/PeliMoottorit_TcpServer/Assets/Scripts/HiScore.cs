/******************************************************************************
 * File        : HiScore.cs
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// HiScore
/// </summary>
public class HiScore : MonoBehaviour {

    /// <summary>
    /// hiScoreTable
    /// </summary>
    [SerializeField] private GameObject hiScoreTable;

    /// <summary>
    /// hiScoreElement
    /// </summary>
    [SerializeField] private GameObject hiScoreElement;

    /// <summary>
    /// hiScoreBoard
    /// </summary>
    [SerializeField] private GameObject hiScoreBoard;

    /// <summary>
    /// hiScoreInput
    /// </summary>
    [SerializeField] private GameObject hiScoreInput;

    /// <summary>
    /// HiScoreElement
    /// </summary>
    List<HiScoreElement> list = new List<HiScoreElement>();

    /// <summary>
    /// hiScoreStore
    /// </summary>
    HiScoreList hiScoreStore;


    /// <summary>
    /// instance
    /// </summary>
    private static HiScore instance;

    /// <summary>
    /// Instance
    /// </summary>
    public static HiScore Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Start() {
        instance = this;

        // 15. Kutsu ReadScore Metodia, metodi palauttaa HiScoreListan, tallenna se luokan jäsenmuuttujaan.
        hiScoreStore = ReadScore();

        // 16. Kutsu UpdateScoreBoard Metodia, välitä lista parametrina
        UpdateScoreBoard(hiScoreStore.HiScoreElementList);

    }

    /// <summary>
    /// Show Hiscore Board
    /// </summary>
    public void Show() {
        hiScoreBoard.SetActive(true);
    }

    /// <summary>
    /// Hide Hiscore Board
    /// </summary>
    public void Hide() {
        hiScoreBoard.SetActive(false);
    }


    /// <summary>
    /// UpdateScoreBoard
    /// </summary>
    /// <param name="aList"></param>
    void UpdateScoreBoard(List<HiScoreElement> list) {


        // 17. Poista kaikki vanhat scoret näkymästä (UI) (Canvas -> Images --> Table, poista tämän alta kaikki GameObjectit )
        foreach (Transform child in hiScoreTable.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        // 18. Päivitä pisteet näkymään, Luo uusi instanssi Template GameObjektista, ja laita se Table
        // GameObjectin lapseksi. Sijoita Text Componentin Text kenttään pelaajan nimi ja pisteet

        foreach (HiScoreElement a in list)
        {
            GameObject o = GameObject.Instantiate(hiScoreElement, hiScoreTable.gameObject.transform);
            o.SetActive(true);
            o.GetComponent<Text>().text = a.Name + " " + a.Score.ToString();
        }


    }


    /// <summary>
    /// Save
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    public void Save(string name, float score) {

        // 20. Lisää uusi HiScore hiScoreStore:een
        HiScoreElement newElement = new HiScoreElement(name, score);
        hiScoreStore.AddToList(newElement);


        // 21. Kutsu SaveScoreBoard metodio tallentaaksesi 
        SaveScoreBoard(hiScoreStore);




        hiScoreInput.gameObject.SetActive(false);

        // 22 Kutsu UpdateScoreBoard metodia
        UpdateScoreBoard(hiScoreStore.HiScoreElementList);
    }

    /// <summary>
    /// ShowInputQuery
    /// </summary>
    /// <param name="score"></param>
    public void ShowInputQuery(float score) {
        hiScoreInput.GetComponent<HiScoreInput>().Score = score;
        hiScoreInput.gameObject.SetActive(true);
    }


    /// <summary>
    /// SaveScoreBoard
    /// </summary>
    /// <param name="sb"></param>
    public void SaveScoreBoard(HiScoreList sb) {

        // 19 .Tallenna scoreBoard Tiedostoon
        // - Luo instanssi BinaryFormatter luokasta 
        // - Luo FileSteam 
        // Vinkki https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity
        // Vinkki https://youtu.be/90CHWxGqCww

    }


    /// <summary>
    /// ReadScore
    /// </summary>
    /// <returns></returns>
    HiScoreList ReadScore() {
        HiScoreList sb = null;

        if (File.Exists(Application.persistentDataPath + "/s002.save")) {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/s002.save", FileMode.Open);
            sb = (HiScoreList)bf.Deserialize(file);
            file.Close();

        } else {


            sb = new HiScoreList();
            sb.HiScoreElementList = new List<HiScoreElement>(); 
            sb.AddToList(new HiScoreElement("ABC", 15));
            sb.AddToList(new HiScoreElement("CCC", 10));
            sb.AddToList(new HiScoreElement("DDD", 5));
            sb.AddToList(new HiScoreElement("EEE", 2));
            SaveScoreBoard(sb);
        }
        return sb;
    }
}
