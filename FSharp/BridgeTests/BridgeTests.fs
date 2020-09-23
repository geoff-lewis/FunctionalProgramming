namespace BridgeTests

module BridgeTests =

    open NUnit.Framework
    open Bridge.DomainTypes
    open Bridge.HandFunctions

    [<SetUp>]
    let Setup () =
        ()

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
                                            {Pip = Ace; Suit = Spades};
                                            ]}
        Assert.That(validateHand thirteenCardHandWithDuplicates, Is.True)