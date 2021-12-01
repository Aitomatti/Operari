/******************************************************************************
 * File        : HiScoreList.cs
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


/// <summary>
/// HiScoreList
/// </summary>
[System.Serializable]
public class HiScoreList{

    /// <summary>
    /// hiScoreElementList
    /// </summary>
    private List<HiScoreElement> hiScoreElementList;

    public List<HiScoreElement> HiScoreElementList
    {
        get
        {
            return this.hiScoreElementList;
        }
        set
        {
            this.hiScoreElementList = value;
        }
    }



    /// <summary>
    /// AddToList
    /// </summary>
    /// <param name="element"></param>
    public void AddToList(HiScoreElement element) {

        if (hiScoreElementList.Count == 0)
        {
            hiScoreElementList.Add(element);
            return;
        }

        int i = 0;

        foreach(HiScoreElement e in hiScoreElementList)
        {
            if (e.Score < element.Score)
            {
                hiScoreElementList.Insert(i, element);

                if (hiScoreElementList.Count > 13)
                {
                    hiScoreElementList.RemoveAt(13);
                }

                return;
            }
            i++;
        }

        if (hiScoreElementList.Count < 13)
        {
            hiScoreElementList.Add(element);
        }

        if (hiScoreElementList.Count > 13)
        {
            hiScoreElementList.RemoveAt(13);
        }

        // 14. Toteuta AddToList Metodi
        // Metodi lisää parametrina saadun HiScoreElement:in hiScoreList:aan
        // Listassa voi olla maksimissaan 10 itemiä, ja lista järjestellään pisteiden mukaan
        // listalla on ensimmäisenä suurimmat pisteet.

    }


}
