import React from "react";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Button } from "@material-ui/core";


const RequiredFieldDialog = (props) => {

    return(
        <div>
            <Dialog
                open={props.open}
                onClose={props.handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">
                {"Required"}
                </DialogTitle>
                <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    {props.RequiredField} is required.
                </DialogContentText>
                </DialogContent>
                <DialogActions>
                <Button onClick={props.handleClose}>Ok</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}

export default RequiredFieldDialog;