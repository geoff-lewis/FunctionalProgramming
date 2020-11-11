namespace BridgeTests

module BridgeTests =

    open NUnit.Framework
    open Bridge.DomainTypes
    open Bridge.HandFunctions

    [<Test>]
    let validateHand_GivenEmptyHand_ReturnsFalse () =
        let emptyHand = { Cards = [] }
        Assert.That(validateHand emptyHand, Is.False)

    [<Test>]
    let validateHand_GivenHandWithLessThanThirteenCards_ReturnsFalse () =
        let singleCardHand = { Cards = [ {Pip = Two; Suit = Spades} ] }
        Assert.That(validateHand singleCardHand, Is.False)

    [<Test>]
    let validateHand_GivenHandWithMoreThanThirteenCards_ReturnsFalse () =
        let fourteenCardHand = { Cards = [ {Pip = Two; Suit = Spades}; 
                                          {Pip = Three; Suit = Spades};
                                          {Pip = Four; Suit = Spades}; 
                                          {Pip = Five; Suit = Spades};
                                          {Pip = Six; Suit = Spades}; 
                                          {Pip = Seven; Suit = Spades};
                                          {Pip = Eight; Suit = Spades}; 
                                          {Pip = Nine; Suit = Spades};
                                          {Pip = Ten; Suit = Spades}; 
                                          {Pip = Jack; Suit = Spades};
                                          {Pip = Queen; Suit = Spades}; 
                                          {Pip = King; Suit = Spades};
                                          {Pip = Ace; Suit = Spades}; 
                                          {Pip = Two; Suit = Diamonds}
                                          ]}
        Assert.That(validateHand fourteenCardHand, Is.False)

    [<Test>]
    let validateHand_GivenHandWithThirteenCardsAndDuplicates_ReturnsFalse () =
        let thirteenCardHandWithDuplicates = { Cards = [ {Pip = Two; Suit = Spades}; 
                                          {Pip = Three; Suit = Spades};
                                          {Pip = Four; Suit = Spades}; 
                                          {Pip = Five; Suit = Spades};
                                          {Pip = Six; Suit = Spades}; 
                                          {Pip = Seven; Suit = Spades};
                                          {Pip = Eight; Suit = Spades}; 
                                          {Pip = Nine; Suit = Spades};
                                          {Pip = Ten; Suit = Spades}; 
                                          {Pip = Jack; Suit = Spades};
                                          {Pip = Queen; Suit = Spades}; 
                                          {Pip = King; Suit = Spades};
                                          {Pip = King; Suit = Spades};
                                          ]}
        Assert.That(validateHand thirteenCardHandWithDuplicates, Is.False)

    [<Test>]
    let validateHand_GivenHandWithThirteenCardsThatAreAllDifferent_ReturnsTrue () =
        let thirteenCardHandWithoutDuplicates = { Cards = [ {Pip = Two; Suit = Spades}; 
                                            {Pip = Three; Suit = Spades};
                                            {Pip = Four; Suit = Spades}; 
                                            {Pip = Five; Suit = Spades};
                                            {Pip = Six; Suit = Spades}; 
                                            {Pip = Seven; Suit = Spades};
                                            {Pip = Eight; Suit = Spades}; 
                                            {Pip = Nine; Suit = Spades};
                                            {Pip = Ten; Suit = Spades}; 
                                            {Pip = Jack; Suit = Spades};
                                            {Pip = Queen; Suit = Spades}; 
                                            {Pip = King; Suit = Spades};
                                            {Pip = Ace; Suit = Spades};
                                            ]}
        Assert.That(validateHand thirteenCardHandWithoutDuplicates, Is.True)

    let singleCardHands =
        seq {
            yield {Cards= [{Pip=Ace; Suit=Hearts}]}
            yield {Cards= [{Pip=Ace; Suit=Spades}]}
            yield {Cards= [{Pip=Two; Suit=Hearts}]}
            yield {Cards= [{Pip=Two; Suit=Spades}]}
        }

    [<TestCaseSource("singleCardHands")>]
    let bestCard_GivenHandWithSingleCard_ReturnsThatCard(h:Hand) =
        let bestCard = bestCardInHand3 h
        Assert.That(bestCard,Is.EqualTo(h.Cards.Head))

    type testData = {Hand:Hand ; Card:Card}

    let twoCardHands = 
        seq {
           yield  { Hand = { Cards= [{Pip=Ace; Suit=Hearts}; {Pip=King; Suit=Hearts}] } ; Card={Pip=Ace; Suit=Hearts} }
            //yield ( { Cards= [{Pip=Ace; Suit=Spades}; {Pip=King; Suit=Spades}] } , {Pip=Ace; Suit=Spades} )
            //yield ( { Cards= [{Pip=King; Suit=Hearts}; {Pip=Ace; Suit=Hearts}] } , {Pip=Ace; Suit=Hearts} )
            //yield ( { Cards= [{Pip=King; Suit=Spades}; {Pip=Ace; Suit=Spades}] } , {Pip=Ace; Suit=Spades} )
            //yield ( { Cards= [{Pip=Ace; Suit=Spades}; {Pip=Ace; Suit=Hearts}] } , {Pip=Ace; Suit=Spades} )
            //yield ( { Cards= [{Pip=Two; Suit=Spades}; {Pip=Ace; Suit=Hearts}] } , {Pip=Two; Suit=Spades} )
            //yield ( { Cards= [{Pip=Ace; Suit=Spades}; {Pip=Two; Suit=Hearts}] } , {Pip=Ace; Suit=Spades} )
            //yield ( { Cards= [{Pip=Two; Suit=Spades}; {Pip=Two; Suit=Hearts}] } , {Pip=Two; Suit=Spades} )
        }

    [<TestCaseSource("twoCardHands")>]
    let bestCard_GivenHandWithTwoCards_ReturnsTheBestCard testData =
        let {Hand=hand; Card=card} = testData
        let bestCard = bestCardInHand2 hand.Cards
        Assert.That(bestCard,Is.EqualTo(card))

    //[<TestCaseSource("twoCardHands")>]
    //let bestCard_GivenHandWithTwoCards_ReturnsTheBestCard(testData:(Hand * Card)) =
    //    let (hand,card) = testData
    //    let bestCard = bestCardInHand2 hand.Cards
    //    Assert.That(bestCard,Is.EqualTo(card))

    //type myType = {int:x1; int:x2}

    //let testSeq = 
    //    seq {
    //        yield (1  1)
    //        yield (2  2)
    //    }

    
    [<TestCaseSource("testSeq")>]
    let testFunc someTestData =
        let (x,y) = someTestData
        printfn "x is %A and y is %A" x |> ignore
        //Assert.That(x, Is.EqualTo(y))

    let genericTupleFn aTuple = 
       let (x,y) = aTuple
       printfn "x is %A and y is %A" x 
       












    let twoCardHands2 = 
        seq {
            yield  { Cards= [{Pip=Ace; Suit=Spades}; {Pip=King; Suit=Spades}] } , {Pip=Ace; Suit=Spades} 
        }

