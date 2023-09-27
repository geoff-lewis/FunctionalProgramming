namespace TennisKata  

type PlayerScore = 
    |Love
    |Fifteen
    |Thirty
    |Forty

type PreDeuce = PreDeuceScore of PlayerScore * PlayerScore

type GameState =
    |PreDeuce of PreDeuce
    |Deuce
    |AdvantageServer
    |AdvantageReturner
    |GameWonServer
    |GameWonReturner

module Game =
    let AwardPoint (playerScore : PlayerScore) =
        match playerScore with 
        | Love -> Fifteen
        | Fifteen -> Thirty
        | Thirty -> Forty
        | Forty -> Forty

    let ScoreServer (gameState : GameState) =
        match gameState with 
        |PreDeuce (PreDeuceScore (ss, rs)) -> 
                match ss,rs with  
                |  Thirty,Forty -> Deuce
                |  Forty,_ -> GameWonServer
                |   s,r ->  PreDeuce (PreDeuceScore(AwardPoint ss,rs))
        |Deuce -> AdvantageServer
        |AdvantageServer -> GameWonServer
        |AdvantageReturner -> Deuce
        |GameWonServer -> GameWonServer
        |GameWonReturner -> GameWonReturner

    let ScoreReturner (gameState : GameState) =
        match gameState with 
        |PreDeuce (PreDeuceScore (ss, rs)) -> 
                match ss,rs with  
                |  Forty,Thirty -> Deuce
                |  _,Forty -> GameWonReturner
                |   s,r ->  PreDeuce (PreDeuceScore(ss, AwardPoint rs))
        |Deuce -> AdvantageReturner
        |AdvantageServer -> Deuce
        |AdvantageReturner -> GameWonReturner
        |GameWonServer -> GameWonServer
        |GameWonReturner -> GameWonReturner

                    

