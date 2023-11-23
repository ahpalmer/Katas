. 'C:\Users\andrewpalmer\Documents\CloudRepos\Katas\src\PowershellKatas\Pi_Iteration\Pi_Iteration_Script.ps1'
Import-Module 'C:\Users\andrewpalmer\Documents\CloudRepos\Katas\src\PowershellKatas\Pi_Iteration'

Describe "Pi Iteration Tests" {
    It "Tests 0.1 epsilon Pi Iteration" {
        Invoke-LeibnizFormula -epsilon 0.1 | Should -Be "[10, 3.0418396189]"
    }
    
    It "Tests 0.01 epsilon Pi Iteration" {
        Invoke-LeibnizFormula -epsilon 0.01 | Should -Be "[100, 3.1315929036]"
    }

    It "Tests 0.001 epsilon Pi Iteration" {
        Invoke-LeibnizFormula -epsilon 0.001 | Should -Be "[1000, 3.1405926538]"
    }
}