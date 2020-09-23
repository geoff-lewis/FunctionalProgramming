namespace Bridge 

    module DomainTypes =

        type Suit =
            | Spades
            | Hearts
            | Diamonds
            | Clubs

        type Pip =
            | Two
            | Three
            | Four
            | Five
            | Six
            | Seven
            | Eight
            | Nine
            | Ten
            | Jack
            | Queen
            | King
            | Ace

        type Card = {Pip:Pip; Suit:Suit}

        type Hand = {Cards: Card list}


