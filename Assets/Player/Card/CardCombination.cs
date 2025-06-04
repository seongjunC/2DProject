using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardCombination
{

    public static CardCombinationEnum CalCombination(List<Card> cards, Player player, out int cardNum)
    {
        int[] EmblemNum = new int[4];
        int[] numbers = new int[13];
        int straightNum = 0;
        int startStraightNum = 0;
        bool IsFlush = false;
        bool IsStraight = false;
        bool IsFullHouse = false;
        bool IsOnePair = false;
        bool IsTwoPair = false;
        bool IsTriple = false;
        bool IsFourCard = false;
        bool IsFiveCard = false;
        int pairNum1 = 0;
        int pairNum2 = 0;
        int tripleNum = 0;
        int fourCardNum = 0;
        int fiveCardNum = 0;
        cardNum = 0;

        // flush에 필요한 카드 개수
        int nowFlushNum = player.flushNum;
        // straight에 필요한 카드 개수
        int nowStraightNum = player.straightNum;


        // 받은 카드를 순회하며 해당 card의 내용을 나눠서 기록
        foreach (Card card in cards)
        {
            EmblemNum[(int)card.Emblem]++;
            numbers[card.CardNum - 1]++;
        }

        // Card들의 플러시 여부를 확인
        foreach (int _emNum in EmblemNum)
        {
            // 만약 카드들의 문양이 플러시를 만족하면 플러시로 판별하고 다음으로 이동
            if (_emNum >= nowFlushNum)
            {
                IsFlush = true;
                break;
            }
        }
        int i = 1;
        // card들의 숫자를 확인
        foreach (int _num in numbers)
        {
            // 해당 숫자가 1개일 경우
            if (_num == 1)
            {
                // 만약 스트레이트의 시작일 경우 스트레이트의 시작 넘버를 기록한다.
                if (straightNum == 0) startStraightNum = i;
                // 연속되는 숫자를 나타내는 straightNum을 +해준다.
                straightNum++;
                // 만일 straightNum이 현재 스트레이트의 수치를 만족하면 스트레이트로 판별하고 다음으로 이동
                if (straightNum >= nowStraightNum)
                {
                    IsStraight = true;
                    break;
                }
                continue;
            }
            else
            {
                // 1개가 아닐경우 스트레이트에 관한 항목을 초기화한다.
                startStraightNum = 0;
                straightNum = 0;
            }

            // 해당 숫자가 2개일 경우 = 페어
            if (_num == 2)
            {
                // 첫번째 페어 항목이 공란일 경우
                if (pairNum1 == 0)
                {
                    // 해당 페어 항목에 채워넣고 일단 OnePair를 true로 만든다.
                    pairNum1 = i;
                    IsOnePair = true;
                    cardNum += pairNum1 * 2;
                    // 트리플을 이미 판별한 경우 풀하우스 이므로 풀하우스로 체크한 후 나간다.
                    if (IsTriple)
                    {
                        IsFullHouse = true;
                        break;
                    }
                }
                // 첫번째 페어 항목이 차있을 경우
                else
                {
                    // 투페어의 숫자를 기록하고 투페어로 판별한 후 탈출한다
                    // ※ 투페어 일 경우 다른 경우의 수가 없기 때문
                    pairNum2 = i;
                    IsTwoPair = true;
                    cardNum += pairNum2 * 2;
                    break;
                }
            }
            // 해당 숫자가 3개일 경우 = 트리플
            else if (_num == 3)
            {
                // 트리플을 true로 하고 해당 숫자를 기록한다.
                IsTriple = true;
                tripleNum = i;
                cardNum += tripleNum * 3;
                // OnePair가 이미 판별된 경우 풀하우스이므로 이를 체크하고 탈출한다.
                if (IsOnePair)
                {
                    IsFullHouse = true;
                    break;
                }
            }
            // 해당 숫자가 4개인 경우 = 포카드
            else if (_num == 4)
            {
                //포카드를 체크하고 탈출한다.
                fourCardNum = i;
                IsFourCard = true;
                cardNum = fourCardNum;
                break;
            }
            else if (_num == 5)
            {
                // 파이브카드를 체크하고 탈출한다.
                fiveCardNum = i;
                IsFiveCard = true;
                cardNum = fiveCardNum;
                break;
            }
            i++;
        }

        if (IsFlush && IsFiveCard) return CardCombinationEnum.FlushFive;

        if (IsFlush && IsFullHouse) return CardCombinationEnum.FlushHouse;

        if (IsFiveCard) return CardCombinationEnum.FiveCard;
        if (IsStraight && IsFlush) return CardCombinationEnum.StraightFlush;
        if (IsFourCard) return CardCombinationEnum.FourCard;
        if (IsFullHouse) return CardCombinationEnum.FullHouse;
        if (IsFlush)
        {
            int j = 1;
            foreach (int _num in numbers)
            {
                cardNum += j * _num;
                j++;
            }
            return CardCombinationEnum.Flush;
        }
        if (IsStraight) return CardCombinationEnum.Straight;
        if (IsTriple) return CardCombinationEnum.Triple;
        if (IsTwoPair) return CardCombinationEnum.TwoPair;
        if (IsOnePair) return CardCombinationEnum.OnePair;

        int k = 0;
        foreach (int _num in numbers)
        {
            if (_num != 0)
            {
                cardNum = _num;
            }
            k++;
        }
        return CardCombinationEnum.HighCard;


    }
}

