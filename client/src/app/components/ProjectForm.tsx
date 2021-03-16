import React, { Dispatch, SetStateAction } from 'react';
import Popup from 'reactjs-popup';
import styles from './projectForm.module.scss';
import { FormControl, TextField, InputLabel, Button } from '@material-ui/core';
import Icon from '@material-ui/core/Icon';
import { useForm } from "react-hook-form";
import { projectService } from "../api/projectService";
import { ProjectFormData } from "../interfaces/interfaces";
import { MuiPickersUtilsProvider, KeyboardDatePicker, } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';

interface Props {
    openModal: boolean,
    setOpenModal: Dispatch<SetStateAction<boolean>>
}
export default function ProjectForm(props: Props) {
    const [startDate, setStartDate] = React.useState<Date | null>(new Date());
    const handleStartDateChange = function (date: Date | null) {
        setStartDate(date);
    }
    const [endDate, setEndDate] = React.useState<Date | null>(new Date());
    const handleEndDateChange = function (date: Date | null) {
        setEndDate(date);
    }
    const { register, handleSubmit } = useForm<ProjectFormData>();
    const onSubmit = async function (data: ProjectFormData) {
        if (endDate != null) {
            data.endDate = endDate;
        }
        if (startDate != null) {
            data.startDate = startDate;
        }
        const response = await projectService.createAsync(data);
        if (response != null) {
            alert(`Success! Project with id ${response.result} was just created.`);
        } else {
            alert("Woops... request failed");
        }
        props.setOpenModal(!props.openModal);
    }

    return (
        <Popup open={props.openModal} onClose={() => props.setOpenModal(false)} modal nested>
            <div className={styles.container}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="grid grid-rows-4 gap-3">
                        <div className="col-span-2">
                            <FormControl fullWidth>
                                <InputLabel htmlFor="project-name"></InputLabel>

                                <TextField id="projectName"
                                    name={"name"}
                                    label="Project Name"
                                    fullWidth variant="outlined"
                                    inputRef={register({ required: true })} />
                            </FormControl>
                        </div>
                        <div className="col-span-2">
                            <FormControl fullWidth>
                                <InputLabel htmlFor="project-description"></InputLabel>
                                <TextField
                                    id="project-description"
                                    name="description"
                                    label="Project Description"
                                    multiline fullWidth
                                    variant="outlined"
                                    inputRef={register({ required: true })} />
                            </FormControl>
                        </div>
                        <div>
                            <FormControl>
                                <InputLabel htmlFor="client-firstname"></InputLabel>
                                <TextField id="client-firstname"
                                    name="clientFirstname"
                                    label="Client Firstname"
                                    variant="outlined"
                                    inputRef={register({ required: true })} />
                            </FormControl>
                        </div>
                        <div>
                            <FormControl>
                                <InputLabel htmlFor="client-lastname"></InputLabel>
                                <TextField id="client-lastname"
                                    name="clientLastname"
                                    label="Client Lastname"
                                    variant="outlined"
                                    inputRef={register({ required: true })} />
                            </FormControl>
                        </div>

                        <div>
                            <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                <KeyboardDatePicker
                                    margin="normal"
                                    disableToolbar
                                    id="startDate"
                                    name="startDate"
                                    label="Start date"
                                    format="MM/dd/yyyy"
                                    value={startDate}
                                    onChange={handleStartDateChange}
                                    KeyboardButtonProps={{
                                        'aria-label': 'change date',
                                    }}
                                />
                            </MuiPickersUtilsProvider>

                        </div>
                        <div>
                            <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                <KeyboardDatePicker
                                    margin="normal"
                                    id="endDate"
                                    name="endDate"
                                    label="End date"
                                    format="MM/dd/yyyy"
                                    value={endDate}
                                    onChange={handleEndDateChange}
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
