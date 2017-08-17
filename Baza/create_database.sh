#!/bin/bash

set -e
set -u

if [ $# != 2 ]; then
    echo "please enter a db host and a user"
    exit 1
fi

export DBHOST=$1
export USER=$2

for f in *.sql
do
	psql \
		-X \
		-U $USER \
		-h $DBHOST \
		-f tbp_baza_shema.sql \
		--echo-all \
		--set AUTOCOMMIT=off \
		--set ON_ERROR_STOP=on \
		tbpfoi1

	psql_exit_status = $?

	if [ $psql_exit_status != 0 ]; then
		echo "psql failed while trying to run this sql script" 1>&2
		exit $psql_exit_status
	fi
done

echo "sql script successful"
exit 0