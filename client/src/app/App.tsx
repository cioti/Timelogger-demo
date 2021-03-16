import * as React from 'react';
import ProjectsView from './views/ProjectsView';
import WorkEntriesView from './views/WorkEntriesView';
import './tailwind.generated.css';
import './styles.scss'

import {
    BrowserRouter as Router,
    Switch,
    Route
} from "react-router-dom";

export default function App() {
    return (
        <>
            <header className="bg-gray-900 text-white flex items-center h-12 w-full">
                <div className="container mx-auto">
                    <a className="navbar-brand" href="/">Timelogger</a>
                </div>
                <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" />
            </header>

            <Router>
                <main>
                    <div className="container mx-auto">
                        <Switch>
                         
                            <Route path="/projects/:projectId/work">
                                <WorkEntriesView />
                            </Route>
                            <Route path="/projects">
                                <ProjectsView />
                            </Route>
                            <Route path="/">
                                <ProjectsView />
                            </Route>
                        </Switch>
                    </div>
                </main>

            </Router>

        </>
    );
}
