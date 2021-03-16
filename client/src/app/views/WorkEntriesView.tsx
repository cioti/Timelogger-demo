import React, { useState, useEffect } from 'react'
import { useParams, useLocation } from "react-router-dom"
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import { workEntryService } from "../api/workEntryService"
import { WorkEntry } from "../interfaces/interfaces"
import WorkEntryForm from "../components/WorkEntryForm"
interface LocationState {
    projectName: string
}
interface Params {
    projectId: string
}
export default function WorkEntriesView() {
    const params = useParams<Params>();
    const location = useLocation<LocationState>();
    const [openModal, setOpenModal] = useState<boolean>(false);

    const [projects, setProjects] = useState<Array<WorkEntry>>([]);
    useEffect(() => {
        const fetchWorkEntries = async () => {
            const response = await workEntryService.listAsync(params.projectId);
            if (response != null) {
                setProjects(response.result);
            }
        };
        fetchWorkEntries();
    }, []);
    return (
        <div>
            <h1 className="title">Work entries for <b>{location.state.projectName}</b></h1>
            <button
                onClick={() => setOpenModal(!openModal)}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            >
                Add entry
            </button>
            <WorkEntryForm projectId={params.projectId} openModal={openModal} setOpenModal={setOpenModal} />
            <TableContainer component={Paper}>
                <Table aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Title</TableCell>
                            <TableCell>Description</TableCell>
                            <TableCell>Hours</TableCell>
                            <TableCell>Date</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {projects.map((row) => (
                            <TableRow key={row.id}>
                                <TableCell>{row.title}</TableCell>
                                <TableCell>{row.description}</TableCell>
                                <TableCell>{row.hours}</TableCell>
                                <TableCell>{row.date}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    )
}
