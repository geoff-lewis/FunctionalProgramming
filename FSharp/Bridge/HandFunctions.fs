namespace Bridge

open DomainTypes

module HandFunctions = 

    type validateHand = Hand -> bool

    let rec listContainsValue<'T when 'T : equality> (x : 'T) (l : 'T list)  =
        match l with
        | [] -> false
        | head :: _ when  x = head -> true
        | _ :: tail -> listContainsValue x tail
     
    let rec listIsDistinct list  =
        match list with
        | [] -> true
        | x :: [] -> true
        | x :: xs when  listContainsValue x xs ->  false
        | _ :: xs -> listIsDistinct xs

    let cardsAreAllDifferent (cards : Card[]) =
        match cards |> Array.distinct  with
        | distinctCards when Array.length distinctCards = Array.length cards -> true
        | _ -> false

    let cardsAreAllDifferentWithArrayEquality (cards : Card[]) =
        match cards |> Array.distinct with
        | distinctCards when distinctCards =  cards -> true
        | _ -> false        

    let validateHand (hand : Hand) =
        match hand.Cards with
        | cards when cards.Length = 13 -> listIsDistinct cards
        | _  -> false
