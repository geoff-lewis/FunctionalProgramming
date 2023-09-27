namespace CypherKata

module Cypher =
    let alphabet = "abcdefghijklmnopqrstuvwxyz";
    let alphabetList = Seq.toList alphabet

    let rec positionInternal character alphabet currentPosition =
        match alphabet with
        | [] -> -1
        | head :: _ when character = head -> currentPosition
        | _ :: tail -> positionInternal character tail (currentPosition + 1)

    let position character =
        positionInternal character alphabetList 0

    let getCharAtPosition position =
        alphabetList[position]

    let rec lookupCharAtOffset totalOffset =
        match totalOffset with
        | x when x <= 25 -> getCharAtPosition totalOffset
        | _ -> lookupCharAtOffset (totalOffset - 26)

    let lookupChar keyCharOffset messageCharOffset = 
        lookupCharAtOffset (keyCharOffset + messageCharOffset)

    let encryptChar keyChar messageChar =
        lookupChar (position keyChar) (position messageChar)

    let encryptMessageArray messageChars KeyChars 

    let encryptMessage (key:string) (message:string) =
        match message with 
        | [] -> []
        | h :: tail -> encryptChar 