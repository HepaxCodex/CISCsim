//
//  BranchPredictionSM.cpp
//  CISCsim
//
//  Created by Andrew Kordik on 10/5/11.
//  Copyright 2011 University of Dayton. All rights reserved.
//

#include <iostream>

#include "BranchPredictionSM.h"




BranchPredictionSM::BranchPredictionSM(){
    this->currentState = NN; //! Set the current State to the initial state
    this->isBranchPredicted = false; //! Predict First branch not taken
}

bool BranchPredictionSM::GetPrediction(){
    switch( this->currentState ) 
    {
        case TT :
            this->isBranchPredicted = true;
        case TN :
            this->isBranchPredicted = true;
        case NT :
            this->isBranchPredicted = true;
        case NN :
            this->isBranchPredicted = false;
    }
    
    return this->isBranchPredicted;
    
}

void BranchPredictionSM::UpdateSM(bool branchWasTaken){
    if( branchWasTaken )
    {
        switch (this->currentState){
            case TT :
                currentState = TT;
                break;
            case TN :
                currentState = NT;
                break;
            case NT :
                currentState = TT;
                break;
            case NN :
                currentState = NT;
                break;
        }
                
    }// if( branchWasTaken )
    else // Branch Was NOT taken
    {
        switch (this->currentState){
            case TT :
                currentState = TN;
                break;
            case TN :
                currentState = NN;
                break;
            case NT :
                currentState = TN;
                break;
            case NN :
                currentState = NN;
                break;
        } // switch (this->currentState)
    }// else [ if(branchWasTaken) ]
    
    
}