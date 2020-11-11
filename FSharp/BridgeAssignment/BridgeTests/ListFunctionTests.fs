namespace BridgeTests

module ListFunctionTests = 

    open NUnit.Framework
    open Bridge.HandFunctions
    open Bridge.DomainTypes

    [<Test>]
    let listContainsValue_GivenEmptyList_ReturnsFalse() =
        Assert.That(listContainsValue {Pip = Two; Suit = Spades} [], Is.False)

    [<Test>]
    let listContainsValue_GivenSingleItemListWithDifferentValue_ReturnsFalse() =
        Assert.That(listContainsValue {Pip = Two; Suit = Spades} [{Pip = Three; Suit = Spades}], Is.False)

    [<Test>]
    let listContainsValue_GivenSingleItemListWithMatchingValue_ReturnsTrue() =
        Assert.That(listContainsValue {Pip = Two; Suit = Spades} [{Pip = Two; Suit = Spades}], Is.True)

    [<Test>]
    let listIsDistinct_GivenEmptyList_ReturnsTrue() = 
        Assert.That(listIsDistinct [], Is.True)

    [<Test>]
    let listIsDistinct_GivenSingleItemList_ReturnsTrue() = 
        Assert.That(listIsDistinct [{Pip=Two; Suit=Spades}], Is.True)

    [<Test>]
    let listIsDistinct_GivenMultipleItemList_WhereAllAreDifferent_ReturnsTrue() =
        let listWithAllDifferentItems =  [ {Pip = Two; Suit = Spades}; 
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
                                            {Pip = Ace; Suit = Spades} ]
        Assert.That(listIsDistinct listWithAllDifferentItems, Is.True)

    [<Test>]
    let listIsDistinct_GivenMultipleItemList_WhereThereAreDuplicaes_ReturnsFalse() =
        let listWithAllDifferentItems =  [ {Pip = Two; Suit = Spades}; 
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
                                            {Pip = King; Suit = Spades} ]
        Assert.That(listIsDistinct listWithAllDifferentItems, Is.False)