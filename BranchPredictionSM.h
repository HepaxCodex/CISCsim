//
//  BranchPredictionSM.h
//  CISCsim
//
//  Created by Andrew Kordik on 10/5/11.
//  Copyright 2011 University of Dayton. All rights reserved.
//

#ifndef CISCsim_BranchPredictionSM_h
#define CISCsim_BranchPredictionSM_h


//! Two Bit Branch Prediction State Machine
/*!
 This class implements a Two-Bit Branch Predicion State
 Machine as described on Page 226 of "Modern Processor Design" by Shen
 */
class BranchPredictionSM
{
private:
    //! Enumeration of Possible States
    enum state 
    {   TT, /*< Taken, Taken */
        NT, /*< Not-taken, Taken */
        TN, /*< Taken, Not-taken */
        NN  /*< Not-taken, Not-taken */
    };
    
    
    //! The Current State of the State Machine
    state currentState;
    
    //! True if the Branch is predicted as taken
    bool isBranchPredicted;
    
public:
    
    //! Constructor
    /*!
     Initialize currentState to be NN
     Initialize isBranchPredicted to false
     */
    BranchPredictionSM();
    
    //! Destructor
    ~BranchPredictionSM();
    
    //! Check to see if a branch should be predicted
    /*!
     @return True if the branch shoule be taken, False Otherwise
     */
    bool GetPrediction();
    
    //! Updates the State of the StateMachine
    /*!
     Updates the State Machine based on the input
     @param branchWasTaken is True of the branch was taken, and False other wise
     */
    void UpdateSM(bool branchWasTaken);
    
    
    
    
};



#endif
