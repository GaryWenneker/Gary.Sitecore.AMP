![](https://mischacommunications.com/wp-content/uploads/2017/10/labimg_640_amp.jpg)

Gary’s Sitecore AMP
===================

## soon to come

This is a sample module which can be used in Sitecore 9.0.1. It is fully build
on the Helix Principles with a little Gary Flavour.

Per feature you can find a readme.md explaining the feature and usage.

In this readme you'll find the following: 1. Setup development environment 2.
Development rules

Setup development environment
=============================

This chapter describes how to setup a local development environment.

Prerequisites
-------------

-   Visual studio 2017

-   Sql Server 2016

-   NodeJS 6+

-   Sitecore 9.0.1

Deploy Solution
---------------

1.  Open the solution

2.  Open the task runner window

3.  Run the \*_LocalDeploy\* task

\* You might get an error regarding sass binding. It is bacause VS might be
using different version of node. Please follow instructions here to fix:
https://ryanhayes.net/synchronize-node-js-install-version-with-visual-studio-2015/

FAQ:
----

-   taskrunner cannot load gulp tasks with an error something like "his usually
    happens because your environment has changed since running `npm install`.
    Run `npm rebuild node-sass` to build the binding for your current
    environment.":
    http://stackoverflow.com/questions/31301582/task-runner-explorer-cant-load-tasks/31444245

Development Rules
=================

This chapter describes some development rules

Apply readme files
------------------

Its required to update the readme files when updating or adding features. This
is needed so the documentation will be kept up to date.

Add Unit test
-------------

\-- Very nice to have!!!!

Meme
----

![](https://i.giphy.com/3o7qE4opCd6f1NJeuY.gif)
