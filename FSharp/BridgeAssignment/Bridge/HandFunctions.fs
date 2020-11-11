namespace Bridge

open DomainTypes

module HandFunctions = 

    type validateHand = Hand -> bool

    let rec listContainsValue x listOfX  =
        match listOfX with
        | [] -> false
        | head :: _ when  x = head -> true
        | _ :: tail -> listContainsValue x tail     

    let rec listIsDistinct list  =
        match list with
        | [] -> true
        | x :: [] -> true
        | x :: xs when listContainsValue x xs ->  false
        | _ :: xs -> listIsDistinct xs

    let cardsAreAllDifferent cards = cards |> Array.distinct = cards 

    let cardsAreAllDifferentWithArrayEquality cards =
        match cards |> Array.distinct with
        | distinctCards when distinctCards =  cards -> true
        | _ -> false        

    let validateHand {Cards = cards} = cards.Length = 13 && listIsDistinct cards

    //let bestCard {Pip = pip1; Suit = suit1} {Pip = pip2; Suit = suit2} =
    //    match suit1 with
    //    | suit2 -> 

    let bestCard card1 card2 = 
        match card1 with
        | c when c = card2 -> invalidArg "card2" "Cards must be different"
        | c when c.Suit > card2.Suit -> card1
        | c when c.Suit < card2.Suit -> card2
        | c when c.Pip > card2.Pip -> card1
        | _ -> card2

    let bestCardInHand1 (hand : Hand) = 
        match hand.Cards  with
        | x :: [] -> x
        | _ -> invalidArg "hand" "can't process this hand" 

    let rec bestCardInHand2 cards = 
        match cards  with
        | x :: [] -> x
        | x :: xs -> xs |> bestCardInHand2 |> bestCard x
        | _ -> invalidArg "hand" "can't process this hand" 

    let bestCardInHand3 {Cards = cards} = 
        match cards  with
        | x :: [] -> x
        | _ -> invalidArg "hand" "can't process this hand" 
  

    let cardValue {Pip = pipValue} =
        match pipValue with
        | Ace -> 4
        | King -> 3
        | Queen -> 2
        | Jack -> 1
        | _ -> 0



