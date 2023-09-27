namespace CypherKataTests
{
    public class Tests
    {

        [Test]
        public void Position_GivenCharacterNotInList_ReturnsMinusOne()
        {
            Assert.That(CypherKata.Cypher.position('4'), Is.EqualTo(-1));
        }

        [Test]
        public void Position_GivenCharacterInList_ReturnsCorrectIndex()
        {
            Assert.That(CypherKata.Cypher.position('a'), Is.EqualTo(0));
        }

        [TestCase('a','a','a')]
        [TestCase('a', 'b', 'b')]
        [TestCase('a', 'z', 'z')]
        [TestCase('z', 'a', 'z')]
        [TestCase('z', 'b', 'a')]
        [TestCase('z', 'z', 'y')]
        public void Encrypt_GivenKeyCharAndMessageChar_ReturnsCorrectEncryptedChar(char keyChar, char messageChar, char expectedChar)
        {
            Assert.That(CypherKata.Cypher.encryptChar(keyChar,messageChar), Is.EqualTo(expectedChar));
        }
    }
}