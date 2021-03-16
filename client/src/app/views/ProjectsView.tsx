import React, { useState, useEffect } from 'react';
import ProjectForm from '../components/ProjectForm'
import { projectService } from "../api/projectService";
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import { Project } from "../interfaces/interfaces"
import { Link } from "react-router-dom"


const useStyles = makeStyles({
	table: {
		minWidth: 650,
	},
});

export default function Projects() {
	const [projects, setProjects] = useState<Array<Project>>([]);
	useEffect(() => {
		const fetchProjects = async () => {
			const response = await projectService.listAsync();
			if (response != null) {
				setProjects(response.result);
			}
		};
		fetchProjects();
	}, []);
	const [openModal, setOpenModal] = useState<boolean>(false);

	const classes = useStyles();
	return (
		<>
			<div className="flex items-center my-6">
				<div className="w-1/2">
					<button
						className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
						onClick={() => setOpenModal(!openModal)}
					>
						Add entry
					</button>
				</div>

				<div className="w-1/2 flex justify-end">
					<form>
						<input className="border rounded-full py-2 px-4" type="search" placeholder="Search" aria-label="Search" />
						<button className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2" type="submit">Search</button>
					</form>
				</div>
			</div>
			<ProjectForm openModal={openModal} setOpenModal={setOpenModal}/>

			<TableContainer component={Paper}>
				<Table className={classes.table} aria-label="simple table">
					<TableHead>
						<TableRow>
							<TableCell></TableCell>
							<TableCell>Name</TableCell>
							<TableCell>Description</TableCell>
							<TableCell>Client</TableCell>
							<TableCell>Start</TableCell>
							<TableCell>End</TableCell>
						</TableRow>
					</TableHead>
					<TableBody>
						{projects.map((row) => (
							<TableRow key={row.id}>
								<TableCell>
									<Link
										className="button"
										to={{
											pathname: `/projects/${row.id}/work`,
											state: { projectName: row.name }
										}}
									>
										Details
									</Link>
								</TableCell>
								<TableCell>{row.name}</TableCell>
								<TableCell>{row.description}</TableCell>
								<TableCell>{`${row.clientFirstname} ${row.clientLastname}`}</TableCell>
								<TableCell>{row.startDate}</TableCell>
								<TableCell>{row.endDate}</TableCell>
							</TableRow>
						))}
					</TableBody>
				</Table>
			</TableContainer>
		</>
	);
}