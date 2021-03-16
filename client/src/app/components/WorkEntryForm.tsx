import React, { Dispatch, SetStateAction } from 'react';
import Popup from 'reactjs-popup';
import { FormControl, TextField, InputLabel, Button } from '@material-ui/core';
import Icon from '@material-ui/core/Icon';
import { useForm } from "react-hook-form";
import { workEntryService } from "../api/workEntryService";
import { WorkEntryFormData } from "../interfaces/interfaces";
import { MuiPickersUtilsProvider, KeyboardDatePicker, } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import styles from "./workEntryForm.module.scss"

interface Props {
    projectId: string,
    openModal: boolean,
    setOpenModal: Dispatch<SetStateAction<boolean>>
}
export default function WorkEntryForm(props: Props) {

    const [date, setDate] = React.useState<Date | null>(new Date());
    const handleDateChange = function (date: Date | null) {
        setDate(date);
    }
    const { register, handleSubmit } = useForm<WorkEntryFormData>();
    const onSubmit = async function (data: WorkEntryFormData) {
        if (date != null) {
            data.date = date;
        }
        const response = await workEntryService.createAsync(props.projectId, data);
        if (response != null) {
            alert(`Success! Work entry with id ${response.result} was just created.`);
        } else {
            alert("Woops... request failed");
        }
        props.setOpenModal(!props.openModal);
    }

    return (
        <Popup open={props.openModal} onClose={() => props.setOpenModal(false)} modal nested>
            <div className={styles.container}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="grid grid-rows-3 gap-3">
                        <div className="col-span-2">
                            <FormControl fullWidth>
                                <InputLabel htmlFor="title"></InputLabel>

                                <TextField id="title"
                                    name="title"
                                    label="Title"
                                    fullWidth variant="outlined"
                                    inputRef={register({ required: true })} />
                            </FormControl>
                        </div>
                        <div className="col-span-2">
                            <FormControl fullWidth>
                                <InputLabel htmlFor="description"></InputLabel>
                                <TextField
                                    id="description"
                                    name="description"
                                    label="Project Description"
                                    multiline fullWidth
                                    variant="outlined"
                                    inputRef={register({ required: true })} />
                            </FormControl>
                        </div>
                        <div>
                            <FormControl>
                                <InputLabel htmlFor="hours"></InputLabel>

                                {/* Had no time to make a numeric input... this should be a numeric input with a step value of 0.5 */}
                                <TextField id="hours"
                                    name="hours"
                                    label="hours"
                                    fullWidth variant="outlined"
                                    inputRef={register({ required: true })} />
                            </FormControl>
                        </div>
                        <div>
                            <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                <KeyboardDatePicker
                                    margin="normal"
                                    disableToolbar
                                    id="date"
                                    name="date"
                                    label="Date"
                                    format="MM/dd/yyyy"
                                    value={date}
                                    onChange={handleDateChange}
                                    KeyboardButtonProps={{
                                        'aria-label': 'change date',
                                    }}
                                />
                            </MuiPickersUtilsProvider>
                        </div>
                    </div>

                    <div className="mt-4 flex justify-center">
                        <Button variant="contained" type="submit" className="mx-auto" color="primary" endIcon={<Icon>send</Icon>}>
                            Submit
                    </Button>
                    </div>
                </form>
            </div>
        </Popup>
    )
}
